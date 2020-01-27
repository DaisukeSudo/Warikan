namespace Warikan.Console.Baker.Domain.Accountant

open Warikan.Domain.SplitBillReport
open Warikan.Domain.Accountant

module SplitBillReportAdjustorBaker =
    let adjust =
        SplitBillReportAdjustor.adjust
            ReportedPaymentClassList.createBy
            TotalPaymentAmount.createBy
            ExtraOrShortage.createBy
