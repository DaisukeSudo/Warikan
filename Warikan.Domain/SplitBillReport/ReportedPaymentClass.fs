namespace Warikan.Domain.SplitBillReport

open Warikan.Domain.DrinkingParty

// ReportedPaymentClass

type GuestPaymentAmount = private GuestPaymentAmount of uint32

module GuestPaymentAmount =
    let create value =
        GuestPaymentAmount value

type ReportedPaymentClass = {
    PaymentClassId : PrescribedPaymentClassId
    PaymentAmount : PrescribedPaymentAmount
    GuestPaymentAmount : GuestPaymentAmount
    GuestsNumber : GuestsNumber
}
    
type ReportedPaymentClassList = {
    Items : ReportedPaymentClass list
}