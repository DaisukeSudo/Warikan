namespace Warikan.Domain.Accountant

open Warikan.Domain.SplitBillReport

module SplitBillReportAdjustor =
    type GetAdditionalPayersCount =
        ReportedPaymentClassList                // I
            -> AdditionalPayersCount            // O
        
    type MakeUpShortfall =
        AdditionalPayersCount                   // I
            -> UnadjustedSplitBillReport        // I
            -> SplitBillReport                  // O

    type AdjustSplitBillReport =
        GetAdditionalPayersCount                // D
            -> MakeUpShortfall                  // D
            -> UnadjustedSplitBillReport        // I
            -> SplitBillReport                  // O
