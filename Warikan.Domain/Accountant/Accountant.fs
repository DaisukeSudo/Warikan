namespace Warikan.Domain.Accountant

open Warikan.Domain.DrinkingParty
open Warikan.Domain.SplitBillReport

module Accountant =
    type CreateUnadjustedSplitBillReport = UnadjustedSplitBillReportFactory.CreateUnadjustedSplitBillReport
    type AdjustSplitBillReport = SplitBillReportAdjustor.AdjustSplitBillReport

    type Calculate =
        CreateUnadjustedSplitBillReport         // D
            -> AdjustSplitBillReport            // D
            -> DrinkingParty                    // I
            -> SplitBillReport                  // O

    let calculate : Calculate =
        fun createUnadjustedSplitBillReport
            adjustSplitBillReport
            drinkingParty
            ->
            failwith "Not Implemented"
