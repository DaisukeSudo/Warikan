namespace Warikan.Domain.SplitBillReport

open Warikan.Domain.DrinkingParty

type OrganizerPaymentAmount =
    private OrganizerPaymentAmount of uint32

module OrganizerPaymentAmount =
    let create v = OrganizerPaymentAmount v
    let value (OrganizerPaymentAmount v) = v

    let createBy (prescribedPaymentAmount : PrescribedPaymentAmount) =
        create (PrescribedPaymentAmount.value prescribedPaymentAmount)


type OrganizerPaymentClass = {
    PaymentClassId          : PrescribedPaymentClassId
    PaymentType             : PrescribedPaymentType
    PaymentAmount           : PrescribedPaymentAmount
    OrganizerPaymentAmount  : OrganizerPaymentAmount
}
        
module OrganizerPaymentClass =
    type CreateOrganizerPaymentClass =
        PrescribedPaymentClassList -> Organizer -> OrganizerPaymentClass

    let create : CreateOrganizerPaymentClass =
        fun paymentClassList organizer ->
            paymentClassList
            |> PrescribedPaymentClassList.findOneById organizer.PaymentClassId 
            |> fun paymentClass -> {
                PaymentClassId          = paymentClass.PaymentClassId
                PaymentType             = paymentClass.PaymentType
                PaymentAmount           = paymentClass.PaymentAmount
                OrganizerPaymentAmount  = OrganizerPaymentAmount.createBy paymentClass.PaymentAmount
            }
