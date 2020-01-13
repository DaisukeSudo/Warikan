namespace Warikan.Domain.SplitBillReport

open Warikan.Domain.DrinkingParty

// SplitBillReport

type OrganizerPaymentAmount = private OrganizerPaymentAmount of uint32

module OrganizerPaymentAmount =
    let create value =
        OrganizerPaymentAmount value
            
type ExtraOrShortage = private ExtraOrShortage of uint32

module ExtraOrShortage =
    let create value =
        ExtraOrShortage value

type SplitBillReport = {
    TotalBilledAmount : TotalBilledAmount
    OrganizerPaymentAmount : OrganizerPaymentAmount
    PaymentClassList : ReportedPaymentClassList
    ExtraOrShortage : ExtraOrShortage
}
