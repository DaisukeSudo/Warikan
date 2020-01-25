namespace Warikan.Domain.DrinkingParty

open System

type DrinkingPartyId =
    private DrinkingPartyId of Guid

module DrinkingPartyId =
    let create v = DrinkingPartyId v
    let value (DrinkingPartyId v) = v


type TotalBilledAmount =
    private TotalBilledAmount of uint32

module TotalBilledAmount =
    let create v = TotalBilledAmount v
    let value (TotalBilledAmount v) = v


type DrinkingParty = {
    DrinkingPartyId     : DrinkingPartyId
    TotalBilledAmount   : TotalBilledAmount
    Organizer           : Organizer
    GuestGroupList      : GuestGroupList
    PaymentClassList    : PrescribedPaymentClassList
}
