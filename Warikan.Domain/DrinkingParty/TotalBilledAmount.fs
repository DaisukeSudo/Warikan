namespace Warikan.Domain.DrinkingParty

open Warikan.Domain.Common

type TotalBilledAmount =
    private TotalBilledAmount of PositiveAmount
    with
    member this.amount() =
        match this with (TotalBilledAmount v) -> v |> PositiveAmount.value

module TotalBilledAmount =
    let create v = TotalBilledAmount v
    let value (TotalBilledAmount v) = v
