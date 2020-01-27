namespace Warikan.Domain.Accountant

open Warikan.Domain.DrinkingParty
open Warikan.Domain.SplitBillReport

module Accountant =
    type CreateUnadjustedSplitBillReport =
        DrinkingParty                           // I
            -> UnadjustedSplitBillReport        // O

    type AdjustSplitBillReport =
        UnadjustedSplitBillReport               // I
            -> SplitBillReport                  // O

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
            drinkingParty
            |> createUnadjustedSplitBillReport
            |> adjustSplitBillReport
