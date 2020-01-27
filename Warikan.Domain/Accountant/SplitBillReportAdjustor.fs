namespace Warikan.Domain.Accountant

open Warikan.Domain.Common
open Warikan.Domain.DrinkingParty
open Warikan.Domain.SplitBillReport

module SplitBillReportAdjustor =
    let convert =
        fun (u: UnadjustedSplitBillReport) ->
            {
                DrinkingPartyId         = u.DrinkingPartyId
                TotalBilledAmount       = u.TotalBilledAmount
                OrganizerPaymentClass   = u.OrganizerPaymentClass
                GuestPaymentClassList   = u.GuestPaymentClassList
                TotalPaymentAmount      = u.TotalPaymentAmount
                ExtraOrShortage         = u.ExtraOrShortage
            }                           

    let makeUpShortfall =
        fun
            createReportedPaymentClassList
            createTotalPaymentAmount
            createExtraOrShortage
            additionalPaymentAmount
            (u: UnadjustedSplitBillReport)
            ->
            let newOrganizerPaymentClass =
                u.OrganizerPaymentClass
                |> AdditionalPaymentAmount.addToOrganizerPaymentClass additionalPaymentAmount

            let newGuestPaymentClassList : GuestPaymentClassList =
                u.GuestPaymentClassList.Items
                |> List.map (
                    fun item ->
                        item
                        |> AdditionalPaymentAmount.addToGuestPaymentClass additionalPaymentAmount
                )
                |> GuestPaymentClassList.create

            let reportedPaymentClassList    = (newOrganizerPaymentClass, newGuestPaymentClassList) |> createReportedPaymentClassList
            let newTotalPaymentAmount       = reportedPaymentClassList  |> createTotalPaymentAmount
            let newExtraOrShortage          = newTotalPaymentAmount     |> createExtraOrShortage u.TotalBilledAmount

            {
                DrinkingPartyId         = u.DrinkingPartyId
                TotalBilledAmount       = u.TotalBilledAmount
                OrganizerPaymentClass   = newOrganizerPaymentClass
                GuestPaymentClassList   = newGuestPaymentClassList
                TotalPaymentAmount      = newTotalPaymentAmount
                ExtraOrShortage         = newExtraOrShortage
            }                           


    type Adjust =
        ReportedPaymentClassList.CreateBy   // D
            -> TotalPaymentAmount.CreateBy  // D
            -> ExtraOrShortage.CreateBy     // D
            -> UnadjustedSplitBillReport    // I
            -> SplitBillReport              // O

    let adjust : Adjust =
        fun
            createReportedPaymentClassList
            createTotalPaymentAmount
            createExtraOrShortage
            u
            ->
            match u.ExtraOrShortage with
            | Extra _ -> convert u
            | Shortage v ->
                let additionalPayersCount =
                    (u.OrganizerPaymentClass, u.GuestPaymentClassList)
                    |> ReportedPaymentClassList.createBy 
                    |> AdditionalPayersCount.createBy

                if additionalPayersCount
                    |> AdditionalPayersCount.value
                    |> (=) 0u
                then
                    convert u
                else
                    makeUpShortfall
                        createReportedPaymentClassList
                        createTotalPaymentAmount
                        createExtraOrShortage
                        (additionalPayersCount |> AdditionalPaymentAmount.createBy v)
                        u
