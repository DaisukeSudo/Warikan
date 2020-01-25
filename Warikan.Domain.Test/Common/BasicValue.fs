namespace Warikan.Domain.Test.Common

open Xunit
open FsUnit.Xunit
open System
open Warikan.Domain.Common

module PositiveAmountTest =

    [<Fact>]
    let ``Create OK1`` () =
        PositiveAmount.create Decimal.Zero
        |> should equal (PositiveAmount.create Decimal.Zero)

    [<Fact>]
    let ``Create OK2`` () =
        PositiveAmount.create Decimal.MaxValue
        |> should equal (PositiveAmount.create Decimal.MaxValue)

    [<Fact>]
    let ``Create OK3`` () =
        PositiveAmount.create 0.00000000001M
        |> should equal (PositiveAmount.create 0.00000000001M)

    [<Fact>]
    let ``Create NG1`` () =
        (fun () -> PositiveAmount.create Decimal.MinusOne |> ignore)
        |> should throw typeof<System.Exception>

    [<Fact>]
    let ``Value`` () =
        PositiveAmount.create 1234567890M |> PositiveAmount.value
        |> should equal 1234567890M

    [<Fact>]
    let ``Sum`` () =
        [1M..10M]
        |> Seq.map PositiveAmount.create
        |> Seq.sum
        |> should equal (PositiveAmount.create 55M)


module NegativeAmountTest =

    [<Fact>]
    let ``Create OK1`` () =
        NegativeAmount.create Decimal.MinusOne
        |> should equal (NegativeAmount.create Decimal.MinusOne)

    [<Fact>]
    let ``Create OK2`` () =
        NegativeAmount.create Decimal.MinValue
        |> should equal (NegativeAmount.create Decimal.MinValue)

    [<Fact>]
    let ``Create OK3`` () =
        NegativeAmount.create -0.00000000001M
        |> should equal (NegativeAmount.create -0.00000000001M)

    [<Fact>]
    let ``Create NG1`` () =
        (fun () -> NegativeAmount.create Decimal.Zero |> ignore)
        |> should throw typeof<System.Exception>

    [<Fact>]
    let ``Create NG2`` () =
        (fun () -> NegativeAmount.create Decimal.One |> ignore)
        |> should throw typeof<System.Exception>

    [<Fact>]
    let ``Value`` () =
        NegativeAmount.create -1234567890M |> NegativeAmount.value
        |> should equal -1234567890M
