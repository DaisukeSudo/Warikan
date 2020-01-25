namespace Warikan.Domain.SplitBillReport

open Warikan.Domain.DrinkingParty

type TotalPaymentAmount =
    private TotalPaymentAmount of uint32

module TotalPaymentAmount =
    let create v = TotalPaymentAmount v
    let value (TotalPaymentAmount v) = v


type ExtraOrShortage =
    private ExtraOrShortage of int

module ExtraOrShortage =
    let create v = ExtraOrShortage v
    let value (ExtraOrShortage v) = v


type UnadjustedSplitBillReport = {
   DrinkingPartyId         : DrinkingPartyId
   TotalBilledAmount       : TotalBilledAmount
   OrganizerPaymentClass   : OrganizerPaymentClass
   GuestPaymentClassList   : GuestPaymentClassList
   TotalPaymentAmount      : TotalPaymentAmount
   ExtraOrShortage         : ExtraOrShortage
}


type SplitBillReport = {
    DrinkingPartyId         : DrinkingPartyId
    TotalBilledAmount       : TotalBilledAmount
    OrganizerPaymentClass   : OrganizerPaymentClass
    GuestPaymentClassList   : GuestPaymentClassList
    TotalPaymentAmount      : TotalPaymentAmount
    ExtraOrShortage         : ExtraOrShortage
}
