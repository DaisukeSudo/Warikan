namespace Warikan.Domain.SplitBillReport

open Warikan.Domain.Common
open Warikan.Domain.DrinkingParty

type ExtraOrShortage =
    | Extra     of PositiveAmount
    | Shortage  of NegativeAmount

module ExtraOrShortage =
    let create (v : decimal) =
        if v >= 0M
        then PositiveAmount.create v |> Extra
        else NegativeAmount.create v |> Shortage

    let value v =
        match v with
        | Extra    v -> v |> PositiveAmount.value
        | Shortage v -> v |> NegativeAmount.value


    type CreateBy =
        TotalBilledAmount               // I
            -> TotalPaymentAmount       // I
            -> ExtraOrShortage          // O

    let createBy : CreateBy =
        fun billedAmount paymentAmount ->
            [
                TotalBilledAmount.value billedAmount
                TotalPaymentAmount.value paymentAmount
            ]
            |> Seq.map PositiveAmount.value
            |> Seq.reduce (-)
            |> create
