namespace Warikan.Domain.DrinkingParty

type PrescribedPaymentClassId =
    private PrescribedPaymentClassId of uint32

module PrescribedPaymentClassId =
    let create v = PrescribedPaymentClassId v
    let value (PrescribedPaymentClassId v) = v


type PrescribedPaymentType =
    | JUST
    | JUST_OR_MORE


type PrescribedPaymentAmount =
    private PrescribedPaymentAmount of uint32

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
        (paymentType    : PrescribedPaymentType)
        (paymentAmount  : PrescribedPaymentAmount)
        (paymentClassId : PrescribedPaymentClassId)
        : PrescribedPaymentClass
        =
        {
            PaymentClassId  = paymentClassId
            PaymentType     = paymentType
            PaymentAmount   = paymentAmount
        }

    let isIdentical
        (paymentClassId : PrescribedPaymentClassId)
        (paymentClass   : PrescribedPaymentClass)
        =
        paymentClass.PaymentClassId = paymentClassId


type PrescribedPaymentClassList = {
    Items : PrescribedPaymentClass list
}

module PrescribedPaymentClassList =
    let getMaxPaymentClassId
        (list : PrescribedPaymentClassList)
        : PrescribedPaymentClassId
        =
        list.Items
        |> Seq.map (fun x -> x.PaymentClassId)
        |> Seq.max

    let newMaxPaymentClassId
        (list : PrescribedPaymentClassList)
        : PrescribedPaymentClassId
        =
        getMaxPaymentClassId list
        |> PrescribedPaymentClassId.value
        |> (+) 1u
        |> PrescribedPaymentClassId.create
        
    let add
        (paymentType    : PrescribedPaymentType)
        (paymentAmount  : PrescribedPaymentAmount)
        (list           : PrescribedPaymentClassList)
        : PrescribedPaymentClassList
        =
        newMaxPaymentClassId list
        |> PrescribedPaymentClass.create paymentType paymentAmount
        |> fun newItem -> list.Items @ [newItem]
        |> fun newItems -> { Items = newItems }

    let findOneById
        (id     : PrescribedPaymentClassId)
        (list   : PrescribedPaymentClassList)
        =
        list.Items
        |> Seq.find (fun x -> x.PaymentClassId = id)
