namespace Warikan.Domain.SplitBillReport

type ReportedPaymentClass =
    | ReportedOrganizerPaymentClass of OrganizerPaymentClass
    | ReportedGuestPaymentClass of GuestPaymentClass


type ReportedPaymentClassList = {
    Items : ReportedPaymentClass list
}

module ReportedPaymentClassList =
    type CreateReportedPaymentClassList =
        OrganizerPaymentClass -> GuestPaymentClassList -> ReportedPaymentClassList

    let createBy : CreateReportedPaymentClassList =
        fun organizerPaymentClass guestPaymentClassList ->
            guestPaymentClassList.Items
            |> List.map (fun x -> ReportedGuestPaymentClass(x))
            |> List.append [ReportedOrganizerPaymentClass(organizerPaymentClass)]
            |> fun items -> { Items = items }
