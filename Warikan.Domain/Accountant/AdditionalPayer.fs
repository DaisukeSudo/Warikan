namespace Warikan.Domain.Accountant

open Warikan.Domain.Common
open Warikan.Domain.DrinkingParty
open Warikan.Domain.SplitBillReport

type AdditionalPayersCount =
    private AdditionalPayersCount of uint32

module AdditionalPayersCount =
    let create v = AdditionalPayersCount v
    let value (AdditionalPayersCount v) = v

    type CreateBy =
        ReportedPaymentClassList
            -> AdditionalPayersCount
    
    let createBy : CreateBy =
        fun paymentClassList ->
            paymentClassList
            |> ReportedPaymentClassList.filterByPaymentType PrescribedPaymentType.JUST_OR_MORE
            |> ReportedPaymentClassList.memberCount
            |> AdditionalPayersCount


type AdditionalPaymentAmount =
    private AdditionalPaymentAmount of PositiveAmount
    
module AdditionalPaymentAmount =
    let create v = AdditionalPaymentAmount v
    let value (AdditionalPaymentAmount v) = v

    type CreateBy =
        NegativeAmount
            -> AdditionalPayersCount
            -> AdditionalPaymentAmount

    let createBy : CreateBy =
        fun negativeAmount additionalPayersCount ->
            [
                negativeAmount |> NegativeAmount.value |> abs
                additionalPayersCount |> AdditionalPayersCount.value |> decimal
            ]
            |> Seq.reduce (/)
            |> PositiveAmount.create
            |> AdditionalPaymentAmount


    type AddToOrganizerPaymentAmount =
        AdditionalPaymentAmount
            -> PrescribedPaymentAmount
            -> OrganizerPaymentAmount

    let addToOrganizerPaymentAmount : AddToOrganizerPaymentAmount =
        fun a b ->
            [
                a |> value
                b |> PrescribedPaymentAmount.value
            ]
            |> Seq.sum
            |> OrganizerPaymentAmount.create

            
    type AddToOrganizerPaymentClass =
        AdditionalPaymentAmount
            -> OrganizerPaymentClass
            -> OrganizerPaymentClass

    let addToOrganizerPaymentClass : AddToOrganizerPaymentClass =
        fun a pc ->
            match pc.PrescribedPaymentType with
            | PrescribedPaymentType.JUST         -> pc
            | PrescribedPaymentType.JUST_OR_MORE ->
                pc
                |> OrganizerPaymentClass.setOrganizerPaymentAmount
                    (pc.PrescribedPaymentAmount |> addToOrganizerPaymentAmount a)


    type AddToGuestPaymentAmount =
        AdditionalPaymentAmount
            -> PrescribedPaymentAmount
            -> GuestPaymentAmount

    let addToGuestPaymentAmount : AddToGuestPaymentAmount =
        fun a b ->
            [
                a |> value
                b |> PrescribedPaymentAmount.value
            ]
            |> Seq.sum
            |> GuestPaymentAmount.create


    type AddToGuestPaymentClass =
        AdditionalPaymentAmount
            -> GuestPaymentClass
            -> GuestPaymentClass

    let addToGuestPaymentClass : AddToGuestPaymentClass =
        fun a pc ->
            match pc.PrescribedPaymentType with
            | PrescribedPaymentType.JUST         -> pc
            | PrescribedPaymentType.JUST_OR_MORE ->
                pc
                |> GuestPaymentClass.setGuestPaymentAmount
                    (pc.PrescribedPaymentAmount |> addToGuestPaymentAmount a)
