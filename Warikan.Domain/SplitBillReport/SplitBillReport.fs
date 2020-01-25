namespace Warikan.Domain.SplitBillReport

open Warikan.Domain.DrinkingParty

type SplitBillReport = {
    DrinkingPartyId         : DrinkingPartyId
    TotalBilledAmount       : TotalBilledAmount
    OrganizerPaymentClass   : OrganizerPaymentClass
    GuestPaymentClassList   : GuestPaymentClassList
    TotalPaymentAmount      : TotalPaymentAmount
    ExtraOrShortage         : ExtraOrShortage
}
