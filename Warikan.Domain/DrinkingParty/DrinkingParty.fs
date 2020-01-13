namespace Warikan.Domain.DrinkingParty

open System

// DrinkingParty

type DrinkingPartyId = private DrinkingPartyId of Guid

module DrinkingPartyId =
    let create value =
        DrinkingPartyId value

type TotalBilledAmount = private TotalBilledAmount of uint32

module TotalBilledAmount =
    let create value =
        TotalBilledAmount value

type DrinkingParty = {
    DrinkingPartyId : DrinkingPartyId
    TotalBilledAmount : TotalBilledAmount
    Organizer : Organizer
    GuestGroupList : GuestGroupList
    PaymentClassList : PrescribedPaymentClassList
}
