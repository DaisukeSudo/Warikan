namespace Warikan.Console.Baker.Domain.Accountant

open Warikan.Domain.DrinkingParty
open Warikan.Domain.SplitBillReport
open Warikan.Domain.Accountant

module AccountantBaker =
    type BakedCalculate =
        DrinkingParty
            -> SplitBillReport

    let calculate : BakedCalculate =
        Accountant.calculate
            UnadjustedSplitBillReportBaker.createBy
            SplitBillReportAdjustorBaker.adjust
