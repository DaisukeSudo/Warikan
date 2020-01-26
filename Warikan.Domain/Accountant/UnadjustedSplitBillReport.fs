namespace Warikan.Domain.Accountant

open Warikan.Domain.DrinkingParty
open Warikan.Domain.SplitBillReport

type UnadjustedSplitBillReport = {
    DrinkingPartyId         : DrinkingPartyId
    TotalBilledAmount       : TotalBilledAmount
    OrganizerPaymentClass   : OrganizerPaymentClass
    GuestPaymentClassList   : GuestPaymentClassList
    TotalPaymentAmount      : TotalPaymentAmount
    ExtraOrShortage         : ExtraOrShortage
}

module UnadjustedSplitBillReport =
    type CreateBy =
        OrganizerPaymentClass.CreateBy              // D
            -> GuestPaymentClassList.CreateBy       // D
            -> ReportedPaymentClassList.CreateBy    // D
            -> TotalPaymentAmount.CreateBy          // D
            -> ExtraOrShortage.CreateBy             // D
            -> DrinkingParty                        // I
            -> UnadjustedSplitBillReport            // O

    let createBy: CreateBy = 
        fun createOrganizerPaymentClass
            createGuestPaymentClassList
            createReportedPaymentClassList
            createTotalPaymentAmount 
            createExtraOrShortage    
            drinkingParty       
            ->
            let organizerPaymentClass       = drinkingParty.Organizer       |> createOrganizerPaymentClass drinkingParty.PaymentClassList
            let guestPaymentClassList       = drinkingParty.GuestGroupList  |> createGuestPaymentClassList drinkingParty.PaymentClassList
            let reportedPaymentClassList    = (organizerPaymentClass, guestPaymentClassList) |> createReportedPaymentClassList
            let totalPaymentAmount          = reportedPaymentClassList      |> createTotalPaymentAmount
            let extraOrShortage             = totalPaymentAmount            |> createExtraOrShortage drinkingParty.TotalBilledAmount
            {
                DrinkingPartyId         = drinkingParty.DrinkingPartyId
                TotalBilledAmount       = drinkingParty.TotalBilledAmount
                OrganizerPaymentClass   = organizerPaymentClass
                GuestPaymentClassList   = guestPaymentClassList
                TotalPaymentAmount      = totalPaymentAmount
                ExtraOrShortage         = extraOrShortage
            }
