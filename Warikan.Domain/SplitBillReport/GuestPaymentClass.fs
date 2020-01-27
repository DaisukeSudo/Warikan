namespace Warikan.Domain.SplitBillReport

open Warikan.Domain.Common
open Warikan.Domain.DrinkingParty

type GuestPaymentAmount =
    private GuestPaymentAmount of PositiveAmount

module GuestPaymentAmount =
    let create v = GuestPaymentAmount v
    let value (GuestPaymentAmount v) = v

    let createBy (prescribedPaymentAmount : PrescribedPaymentAmount) =
        prescribedPaymentAmount
        |> PrescribedPaymentAmount.value
        |> create


type GuestPaymentClass = {
    PrescribedPaymentClassId    : PrescribedPaymentClassId
    PrescribedPaymentType       : PrescribedPaymentType
    PrescribedPaymentAmount     : PrescribedPaymentAmount
    GuestPaymentAmount          : GuestPaymentAmount
    GuestsCount                 : GuestsCount
}

module GuestPaymentClass =
    type CreateBy =
        PrescribedPaymentClassList
            -> GuestGroup
            -> GuestPaymentClass

    let createBy : CreateBy =
        fun paymentClassList guestGroup ->
            paymentClassList
            |> PrescribedPaymentClassList.findOneById guestGroup.PaymentClassId 
            |> fun paymentClass -> {
                PrescribedPaymentClassId    = paymentClass.PaymentClassId
                PrescribedPaymentType       = paymentClass.PaymentType
                PrescribedPaymentAmount     = paymentClass.PaymentAmount
                GuestPaymentAmount          = paymentClass.PaymentAmount |> GuestPaymentAmount.createBy
                GuestsCount                 = guestGroup.GuestsCount
            }

    type ClassPaymentAmountValue =
        GuestPaymentClass
            -> PositiveAmount

    let classPaymentAmountValue : ClassPaymentAmountValue =
        fun c ->
            [
                c.GuestPaymentAmount |> GuestPaymentAmount.value |> PositiveAmount.value
                c.GuestsCount        |> GuestsCount.value        |> decimal
            ]
            |> Seq.reduce (*)
            |> PositiveAmount.create


    type SetGuestPaymentAmount = 
        GuestPaymentAmount
            -> GuestPaymentClass
            -> GuestPaymentClass

    let setGuestPaymentAmount : SetGuestPaymentAmount =
        fun newPaymentAmount pc ->
            {
                PrescribedPaymentClassId    = pc.PrescribedPaymentClassId
                PrescribedPaymentType       = pc.PrescribedPaymentType
                PrescribedPaymentAmount     = pc.PrescribedPaymentAmount
                GuestPaymentAmount          = newPaymentAmount
                GuestsCount                 = pc.GuestsCount
            }


type GuestPaymentClassList = {
    Items : GuestPaymentClass list
}

module GuestPaymentClassList =
    type CreateBy =
        PrescribedPaymentClassList
            -> GuestGroupList
            -> GuestPaymentClassList

    let createBy : CreateBy =
        fun paymentClassList guestGroupList ->
            guestGroupList.Items
            |> List.map (GuestPaymentClass.createBy paymentClassList)
            |> fun items -> { Items = items }
