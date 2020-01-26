namespace Warikan.Domain.SplitBillReport

open Warikan.Domain.Common
open Warikan.Domain.DrinkingParty

type OrganizerPaymentAmount =
    private OrganizerPaymentAmount of PositiveAmount

module OrganizerPaymentAmount =
    let create v = OrganizerPaymentAmount v
    let value (OrganizerPaymentAmount v) = v

    let createBy (prescribedPaymentAmount: PrescribedPaymentAmount) =
        prescribedPaymentAmount
        |> PrescribedPaymentAmount.value
        |> create


type OrganizerPaymentClass = {
    PrescribedPaymentClassId    : PrescribedPaymentClassId
    PrescribedPaymentType       : PrescribedPaymentType
    PrescribedPaymentAmount     : PrescribedPaymentAmount
    OrganizerPaymentAmount      : OrganizerPaymentAmount
}

module OrganizerPaymentClass =
    type CreateBy =
        PrescribedPaymentClassList
            -> Organizer
            -> OrganizerPaymentClass

    let createBy: CreateBy =
        fun paymentClassList organizer ->
            paymentClassList
            |> PrescribedPaymentClassList.findOneById organizer.PaymentClassId 
            |> fun paymentClass -> {
                PrescribedPaymentClassId    = paymentClass.PaymentClassId
                PrescribedPaymentType       = paymentClass.PaymentType
                PrescribedPaymentAmount     = paymentClass.PaymentAmount
                OrganizerPaymentAmount      = paymentClass.PaymentAmount |> OrganizerPaymentAmount.createBy
            }

    type ClassPaymentAmountValue =
        OrganizerPaymentClass
            -> PositiveAmount

    let classPaymentAmountValue: ClassPaymentAmountValue =
        fun c ->
            c.OrganizerPaymentAmount
            |> OrganizerPaymentAmount.value


    type SetOrganizerPaymentAmount = 
        OrganizerPaymentAmount
            -> OrganizerPaymentClass
            -> OrganizerPaymentClass

    let setOrganizerPaymentAmount: SetOrganizerPaymentAmount =
        fun newPaymentAmount pc ->
            {
                PrescribedPaymentClassId    = pc.PrescribedPaymentClassId
                PrescribedPaymentType       = pc.PrescribedPaymentType
                PrescribedPaymentAmount     = pc.PrescribedPaymentAmount
                OrganizerPaymentAmount      = newPaymentAmount
            }
