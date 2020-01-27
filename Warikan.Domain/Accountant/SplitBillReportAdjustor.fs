namespace Warikan.Domain.Accountant

open Warikan.Domain.Common
open Warikan.Domain.DrinkingParty
open Warikan.Domain.SplitBillReport

module SplitBillReportAdjustor =
    type MakeUpShortfall =
        NegativeAmount
            -> UnadjustedSplitBillReport
            -> SplitBillReport

    let makeUpShortfall : MakeUpShortfall =
        fun v u ->
            let additionalPaymentAmount =
                (u.OrganizerPaymentClass, u.GuestPaymentClassList)
                |> ReportedPaymentClassList.createBy 
                |> AdditionalPayersCount.createBy
                |> AdditionalPaymentAmount.createBy v

            let newOrganizerPaymentClass =
                u.OrganizerPaymentClass
                |> AdditionalPaymentAmount.addToOrganizerPaymentClass additionalPaymentAmount

            let newGuestPaymentClassList : GuestPaymentClassList =
                u.GuestPaymentClassList.Items
                |> List.map (
                    fun item ->
                        match item with
                        | item when item.PrescribedPaymentType = PrescribedPaymentType.JUST_OR_MORE ->
                            item
                            |> AdditionalPaymentAmount.addToGuestPaymentClass additionalPaymentAmount
                        | item -> item
                )
                |> (fun items -> { Items = items })

            {
                DrinkingPartyId         = u.DrinkingPartyId
                TotalBilledAmount       = u.TotalBilledAmount
                OrganizerPaymentClass   = newOrganizerPaymentClass
                GuestPaymentClassList   = newGuestPaymentClassList
                TotalPaymentAmount      = u.TotalPaymentAmount
                ExtraOrShortage         = u.ExtraOrShortage
            }                           


    type Adjust =
        UnadjustedSplitBillReport
            -> SplitBillReport

    let adjust : Adjust =
        fun u ->
            match u.ExtraOrShortage with
            | Extra _ ->
                {
                    DrinkingPartyId         = u.DrinkingPartyId
                    TotalBilledAmount       = u.TotalBilledAmount
                    OrganizerPaymentClass   = u.OrganizerPaymentClass
                    GuestPaymentClassList   = u.GuestPaymentClassList
                    TotalPaymentAmount      = u.TotalPaymentAmount
                    ExtraOrShortage         = u.ExtraOrShortage
                }                           
            | Shortage v -> makeUpShortfall v u 
