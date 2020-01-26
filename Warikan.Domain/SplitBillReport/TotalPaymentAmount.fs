namespace Warikan.Domain.SplitBillReport

open Warikan.Domain.Common

type TotalPaymentAmount =
    private TotalPaymentAmount of PositiveAmount
    with
    member this.amount() =
        match this with (TotalPaymentAmount v) -> v |> PositiveAmount.value

module TotalPaymentAmount =
    let create v = TotalPaymentAmount v
    let value (TotalPaymentAmount v) = v

    type CreateBy =
        ReportedPaymentClassList    // I
            -> TotalPaymentAmount   // O  

    let createBy : CreateBy =
        fun paymentClassList -> 
            ReportedPaymentClassList.totalPaymentAmountValue paymentClassList
            |> TotalPaymentAmount
