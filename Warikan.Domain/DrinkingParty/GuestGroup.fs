namespace Warikan.Domain.DrinkingParty

type GuestsCount =
    private GuestsCount of uint32

module GuestsCount =
    let create v = GuestsCount v
    let value (GuestsCount v) = v


type GuestGroup = {
    PaymentClassId  : PrescribedPaymentClassId
    GuestsCount     : GuestsCount
}

module GuestGroup =
    let create
        (guestsCount    : GuestsCount               )
        (paymentClassId : PrescribedPaymentClassId  )
        : GuestGroup
        =
        {
            PaymentClassId  = paymentClassId
            GuestsCount     = guestsCount
        }


type GuestGroupList = {
    Items : GuestGroup list
}
