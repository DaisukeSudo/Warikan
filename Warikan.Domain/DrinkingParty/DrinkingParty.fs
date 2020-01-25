namespace Warikan.Domain.DrinkingParty

open System
open Warikan.Domain.Common

type DrinkingPartyId =
    private DrinkingPartyId of Guid

module DrinkingPartyId =
    let create v = DrinkingPartyId v
    let value (DrinkingPartyId v) = v


type DrinkingParty = {
    DrinkingPartyId     : DrinkingPartyId
    TotalBilledAmount   : TotalBilledAmount
    Organizer           : Organizer
    GuestGroupList      : GuestGroupList
    PaymentClassList    : PrescribedPaymentClassList
}

module DrinkingParty =
    let create
        (totalBilledAmount   : TotalBilledAmount            )
        (organizer           : Organizer                    )
        (guestGroupList      : GuestGroupList               )
        (paymentClassList    : PrescribedPaymentClassList   )
        (drinkingPartyId     : DrinkingPartyId              )
        : DrinkingParty
        =
        {
            DrinkingPartyId     = drinkingPartyId
            TotalBilledAmount   = totalBilledAmount
            Organizer           = organizer
            GuestGroupList      = guestGroupList
            PaymentClassList    = paymentClassList
        }
