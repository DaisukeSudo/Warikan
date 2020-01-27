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
            -> OrganizerPaymentAmount
            -> OrganizerPaymentAmount

    let addToOrganizerPaymentAmount : AddToOrganizerPaymentAmount =
        fun a b ->
            [
                a |> value
                b |> OrganizerPaymentAmount.value
            ]
            |> Seq.sum
            |> OrganizerPaymentAmount.create

            
    type AddToOrganizerPaymentClass =
        AdditionalPaymentAmount
            -> OrganizerPaymentClass
            -> OrganizerPaymentClass

    let addToOrganizerPaymentClass : AddToOrganizerPaymentClass =
        fun a pc ->
            pc
            |> OrganizerPaymentClass.setOrganizerPaymentAmount
                (pc.OrganizerPaymentAmount |> addToOrganizerPaymentAmount a)


    type AddToGuestPaymentAmount =
        AdditionalPaymentAmount
            -> GuestPaymentAmount
            -> GuestPaymentAmount

    let addToGuestPaymentAmount : AddToGuestPaymentAmount =
        fun a b ->
            [
                a |> value
                b |> GuestPaymentAmount.value
            ]
            |> Seq.sum
            |> GuestPaymentAmount.create


    type AddToGuestPaymentClass =
        AdditionalPaymentAmount
            -> GuestPaymentClass
            -> GuestPaymentClass

    let addToGuestPaymentClass : AddToGuestPaymentClass =
        fun a pc ->
            pc
            |> GuestPaymentClass.setGuestPaymentAmount
                (pc.GuestPaymentAmount |> addToGuestPaymentAmount a)

