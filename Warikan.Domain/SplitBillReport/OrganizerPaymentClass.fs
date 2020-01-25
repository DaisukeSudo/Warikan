namespace Warikan.Domain.SplitBillReport

open Warikan.Domain.Common
open Warikan.Domain.DrinkingParty

type OrganizerPaymentAmount =
    private OrganizerPaymentAmount of PositiveAmount

module OrganizerPaymentAmount =
    let create v = OrganizerPaymentAmount v
    let value (OrganizerPaymentAmount v) = v

    let createBy (prescribedPaymentAmount : PrescribedPaymentAmount) =
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

    let createBy : CreateBy =
        fun paymentClassList organizer ->
            paymentClassList
            |> PrescribedPaymentClassList.findOneById organizer.PaymentClassId 
            |> fun paymentClass -> {
                PrescribedPaymentClassId    = paymentClass.PaymentClassId
                PrescribedPaymentType       = paymentClass.PaymentType
                PrescribedPaymentAmount     = paymentClass.PaymentAmount
                OrganizerPaymentAmount      = paymentClass.PaymentAmount |> OrganizerPaymentAmount.createBy
            }

    let classPaymentAmountValue (c: OrganizerPaymentClass) =
        c.OrganizerPaymentAmount
        |> OrganizerPaymentAmount.value
