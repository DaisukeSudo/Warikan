namespace Warikan.Domain.SplitBillReport

open Warikan.Domain.Common

type ReportedPaymentClass =
    | ReportedOrganizerPaymentClass of OrganizerPaymentClass
    | ReportedGuestPaymentClass     of GuestPaymentClass

module ReportedPaymentClass =
    let classPaymentAmountValue paymentClass =
        match paymentClass with
        | ReportedOrganizerPaymentClass c -> c |> OrganizerPaymentClass.classPaymentAmountValue
        | ReportedGuestPaymentClass     c -> c |> GuestPaymentClass.classPaymentAmountValue


type ReportedPaymentClassList = {
    Items : ReportedPaymentClass list
}

module ReportedPaymentClassList =
    type CreateBy =
        OrganizerPaymentClass
            -> GuestPaymentClassList
            -> ReportedPaymentClassList

    let createBy : CreateBy =
        fun organizerPaymentClass guestPaymentClassList ->
            guestPaymentClassList.Items
            |> List.map (fun x -> ReportedGuestPaymentClass(x))
            |> List.append [ReportedOrganizerPaymentClass(organizerPaymentClass)]
            |> fun items -> { Items = items }

    type TotalPaymentAmountValue =
        ReportedPaymentClassList
            -> PositiveAmount

    let totalPaymentAmountValue : TotalPaymentAmountValue =
        fun paymentClassList ->
            paymentClassList.Items
            |> Seq.map (fun x -> x |> ReportedPaymentClass.classPaymentAmountValue)
            |> Seq.sum
