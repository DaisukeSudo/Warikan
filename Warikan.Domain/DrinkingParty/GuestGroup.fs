namespace Warikan.Domain.DrinkingParty

// GuestGroup

type GuestsNumber = private GuestsNumber of uint32

module GuestsNumber =
    let create value =
        GuestsNumber value

type GuestGroup = {
    PaymentClassId : PrescribedPaymentClassId
    GuestsNumber : GuestsNumber
}

type GuestGroupList = {
    Items : GuestGroup list
}

// Organizer

type Organizer = {
    PaymentClassId : PrescribedPaymentClassId
}
