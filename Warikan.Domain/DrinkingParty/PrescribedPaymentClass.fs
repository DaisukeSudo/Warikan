namespace Warikan.Domain.DrinkingParty

open Warikan.Domain.Common

type PrescribedPaymentClassId =
    private PrescribedPaymentClassId of uint32

module PrescribedPaymentClassId =
    let create v = PrescribedPaymentClassId v
    let value (PrescribedPaymentClassId v) = v


type PrescribedPaymentType =
    | JUST
    | JUST_OR_MORE


type PrescribedPaymentAmount =
    private PrescribedPaymentAmount of PositiveAmount

module PrescribedPaymentAmount =
    let create v = PrescribedPaymentAmount v
    let value (PrescribedPaymentAmount v) = v


type PrescribedPaymentClass = {
    PaymentClassId  : PrescribedPaymentClassId
    PaymentType     : PrescribedPaymentType
    PaymentAmount   : PrescribedPaymentAmount
}

module PrescribedPaymentClass =
    let create
        (paymentType    : PrescribedPaymentType     )
        (paymentAmount  : PrescribedPaymentAmount   )
        (paymentClassId : PrescribedPaymentClassId  )
        : PrescribedPaymentClass
        =
        {
            PaymentClassId  = paymentClassId
            PaymentType     = paymentType
            PaymentAmount   = paymentAmount
        }


type PrescribedPaymentClassList = {
    Items : PrescribedPaymentClass list
}

module PrescribedPaymentClassList =
    let private getMaxPaymentClassId
        (list : PrescribedPaymentClassList)
        : PrescribedPaymentClassId
        =
        list.Items
        |> Seq.map (fun x -> x.PaymentClassId)
        |> Seq.max

    let private newMaxPaymentClassId
        (list : PrescribedPaymentClassList)
        : PrescribedPaymentClassId
        =
        getMaxPaymentClassId list
        |> PrescribedPaymentClassId.value
        |> (+) 1u
        |> PrescribedPaymentClassId.create

    type Add =
        PrescribedPaymentType
            -> PrescribedPaymentAmount
            -> PrescribedPaymentClassList
            -> PrescribedPaymentClassList
        
    let add : Add =
        fun paymentType paymentAmount list ->
            newMaxPaymentClassId list
            |> PrescribedPaymentClass.create paymentType paymentAmount
            |> fun newItem -> { Items = list.Items @ [newItem] }

    type FindOneById =
        PrescribedPaymentClassId
            -> PrescribedPaymentClassList
            -> PrescribedPaymentClass

    let findOneById : FindOneById =
        fun id list ->
            list.Items
            |> Seq.find (fun x -> x.PaymentClassId = id)
