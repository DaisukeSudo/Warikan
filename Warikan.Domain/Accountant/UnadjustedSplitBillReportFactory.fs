namespace Warikan.Domain.Accountant

open Warikan.Domain.DrinkingParty
open Warikan.Domain.SplitBillReport

module UnadjustedSplitBillReportFactory =
    type CreateOrganizerPaymentClass = OrganizerPaymentClass.CreateOrganizerPaymentClass
    type CreateGuestPaymentClassList = GuestPaymentClassList.CreateGuestPaymentClassList
    type CreateReportedPaymentClassList = ReportedPaymentClassList.CreateReportedPaymentClassList

    type CalculateTotalPaymentAmount =
        ReportedPaymentClassList ->             // I
            TotalPaymentAmount                  // O

    type CalculateExtraOrShortage =
        TotalBilledAmount                       // I
            -> TotalPaymentAmount               // I
            -> ExtraOrShortage                  // O

    type CreateUnadjustedSplitBillReport =
        CreateOrganizerPaymentClass             // D
            -> CreateGuestPaymentClassList      // D
            -> CreateReportedPaymentClassList   // D
            -> CalculateTotalPaymentAmount      // D
            -> CalculateExtraOrShortage         // D
            -> DrinkingParty                    // I
            -> UnadjustedSplitBillReport        // O

    let calculateTotalPaymentAmount : CalculateTotalPaymentAmount =
        fun reportedPaymentClassList ->
            reportedPaymentClassList.Items
            |> Seq.map (fun x -> 
                match x with
                | ReportedOrganizerPaymentClass x -> OrganizerPaymentAmount.value x.OrganizerPaymentAmount
                | ReportedGuestPaymentClass x -> GuestPaymentAmount.value x.GuestPaymentAmount
            )
            |> Seq.sum
            |> TotalPaymentAmount.create

    let calculateExtraOrShortage : CalculateExtraOrShortage =
        fun billedAmount paymentAmount ->
            [
                TotalBilledAmount.value billedAmount
                TotalPaymentAmount.value paymentAmount
            ]
            |> Seq.map int
            |> Seq.reduce (-)
            |> ExtraOrShortage.create

    let createUnadjustedSplitBillReport : CreateUnadjustedSplitBillReport = 
        fun createOrganizerPaymentClass
            createGuestPaymentClassList
            createReportedPaymentClassList
            calculateTotalPaymentAmount 
            calculateExtraOrShortage    
            drinkingParty       
            ->
            failwith "Not Implemented"
