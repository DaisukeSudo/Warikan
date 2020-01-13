namespace Warikan.Domain

open Warikan.Domain.DrinkingParty
open Warikan.Domain.SplitBillReport

module Accountant =

    type Calculate =
        DrinkingParty -> SplitBillReport
