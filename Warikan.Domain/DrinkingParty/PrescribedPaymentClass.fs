namespace Warikan.Domain.DrinkingParty

// PrescribedPaymentClass

type PrescribedPaymentClassId = private PrescribedPaymentClassId of uint32

module PrescribedPaymentClassId =
    let create value =
        PrescribedPaymentClassId value

type PrescribedPaymentType =
    | JUST
    | JUST_OR_MORE

type PrescribedPaymentAmount = private PrescribedPaymentAmount of uint32

module PrescribedPaymentAmount =
    let create value =
        PrescribedPaymentAmount value

type PrescribedPaymentClass = {
    PaymentClassId : PrescribedPaymentClassId
    PaymentAmount : PrescribedPaymentAmount
    PaymentType : PrescribedPaymentType
}

type PrescribedPaymentClassList = {
    Items : PrescribedPaymentClass list
}
