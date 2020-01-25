namespace Warikan.Domain.DrinkingParty

type Organizer = {
    PaymentClassId : PrescribedPaymentClassId
}

module Organizer =
    let create
        (paymentClassId : PrescribedPaymentClassId)
        : Organizer
        =
        {
            PaymentClassId  = paymentClassId
        }
