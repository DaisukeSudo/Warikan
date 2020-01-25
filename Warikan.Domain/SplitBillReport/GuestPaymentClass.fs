namespace Warikan.Domain.SplitBillReport

open Warikan.Domain.DrinkingParty

type GuestPaymentAmount =
    private GuestPaymentAmount of uint32

module GuestPaymentAmount =
    let create v = GuestPaymentAmount v
    let value (GuestPaymentAmount v) = v

    let createBy (prescribedPaymentAmount : PrescribedPaymentAmount) =
        create (PrescribedPaymentAmount.value prescribedPaymentAmount)


type GuestPaymentClass = {
    PaymentClassId      : PrescribedPaymentClassId
    PaymentType         : PrescribedPaymentType
    PaymentAmount       : PrescribedPaymentAmount
    GuestPaymentAmount  : GuestPaymentAmount
    GuestsCount         : GuestsCount
}

module GuestPaymentClass =
    type CreateGuestPaymentClass =
        PrescribedPaymentClassList -> GuestGroup -> GuestPaymentClass

    let createBy : CreateGuestPaymentClass =
        fun paymentClassList guestGroup ->
            paymentClassList
            |> PrescribedPaymentClassList.findOneById guestGroup.PaymentClassId 
            |> fun paymentClass -> {
                PaymentClassId          = paymentClass.PaymentClassId
                PaymentType             = paymentClass.PaymentType
                PaymentAmount           = paymentClass.PaymentAmount
                GuestPaymentAmount      = GuestPaymentAmount.createBy paymentClass.PaymentAmount
                GuestsCount             = guestGroup.GuestsCount
            }


type GuestPaymentClassList = {
    Items : GuestPaymentClass list
}

module GuestPaymentClassList =
    type CreateGuestPaymentClassList =
        PrescribedPaymentClassList -> GuestGroupList -> GuestPaymentClassList

    let createBy : CreateGuestPaymentClassList =
        fun paymentClassList guestGroupList ->
            guestGroupList.Items
            |> List.map (fun item -> GuestPaymentClass.createBy paymentClassList item)
            |> fun items -> {
                Items = items
            }
