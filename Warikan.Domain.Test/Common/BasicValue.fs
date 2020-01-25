namespace Warikan.Domain.Test.Common

open Xunit
open FsUnit.Xunit
open System
open Warikan.Domain.Common

module PositiveAmountTest =

    [<Fact>]
    let ``Create OK1`` () =
        PositiveAmount.create Decimal.Zero |> PositiveAmount.value |> should equal Decimal.Zero

    [<Fact>]
    let ``Create OK2`` () =
        PositiveAmount.create Decimal.MaxValue |> PositiveAmount.value |> should equal Decimal.MaxValue

    [<Fact>]
    let ``Create OK3`` () =
        PositiveAmount.create 0.00000000001M |> PositiveAmount.value |> should equal 0.00000000001M

    [<Fact>]
    let ``Create NG1`` () =
        (fun () -> PositiveAmount.create Decimal.MinusOne |> ignore) |> should throw typeof<System.Exception>


module NegativeAmountTest =

    [<Fact>]
    let ``Create OK1`` () =
        NegativeAmount.create Decimal.MinusOne |> NegativeAmount.value |> should equal Decimal.MinusOne

    [<Fact>]
    let ``Create OK2`` () =
        NegativeAmount.create Decimal.MinValue |> NegativeAmount.value |> should equal Decimal.MinValue

    [<Fact>]
    let ``Create OK3`` () =
        NegativeAmount.create -0.00000000001M |> NegativeAmount.value |> should equal -0.00000000001M

    [<Fact>]
    let ``Create NG1`` () =
        (fun () -> NegativeAmount.create Decimal.Zero |> ignore) |> should throw typeof<System.Exception>

    [<Fact>]
    let ``Create NG2`` () =
        (fun () -> NegativeAmount.create Decimal.One |> ignore) |> should throw typeof<System.Exception>
