namespace Warikan.Console.Baker.Domain.Accountant

open Warikan.Domain.SplitBillReport
open Warikan.Domain.Accountant

module UnadjustedSplitBillReportBaker =
    let createBy =
        UnadjustedSplitBillReport.createBy
            OrganizerPaymentClass.createBy
            GuestPaymentClassList.createBy
            ReportedPaymentClassList.createBy
            TotalPaymentAmount.createBy
            ExtraOrShortage.createBy
