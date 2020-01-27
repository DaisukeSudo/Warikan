namespace Warikan.Domain.SplitBillReport

open Warikan.Domain.Common
open Warikan.Domain.DrinkingParty

type ReportedPaymentClass =
    | ReportedOrganizerPaymentClass of OrganizerPaymentClass
    | ReportedGuestPaymentClass     of GuestPaymentClass

module ReportedPaymentClass =
    type ClassPaymentAmountValue =
        ReportedPaymentClass
            -> PositiveAmount

    let classPaymentAmountValue : ClassPaymentAmountValue =
        fun paymentClass ->
            match paymentClass with
            | ReportedOrganizerPaymentClass c -> c |> OrganizerPaymentClass.classPaymentAmountValue
            | ReportedGuestPaymentClass     c -> c |> GuestPaymentClass.classPaymentAmountValue

    type ConditionPaymentType =
        Warikan.Domain.DrinkingParty.PrescribedPaymentType
            -> ReportedPaymentClass
            -> bool

    let conditionPaymentType : ConditionPaymentType =
        fun paymentType paymentClass ->
            (
                match paymentClass with
                | ReportedOrganizerPaymentClass c -> c.PrescribedPaymentType
                | ReportedGuestPaymentClass     c -> c.PrescribedPaymentType
            )
            |> (=) paymentType

    type MemberCount =
        ReportedPaymentClass
            -> uint32

    let memberCount : MemberCount =
        fun paymentClass ->
            match paymentClass with
            | ReportedOrganizerPaymentClass _ -> 1u
            | ReportedGuestPaymentClass     c -> c.GuestsCount |> GuestsCount.value


module ReportedOrganizerPaymentClass =
    let create v = ReportedOrganizerPaymentClass v

module ReportedGuestPaymentClass =
    let create v = ReportedGuestPaymentClass v


type ReportedPaymentClassList = {
    Items : ReportedPaymentClass list
}

module ReportedPaymentClassList =
    type CreateBy =
        OrganizerPaymentClass * GuestPaymentClassList
            -> ReportedPaymentClassList

    let createBy : CreateBy =
        fun (organizerPaymentClass, guestPaymentClassList) ->
            guestPaymentClassList.Items
            |> List.map ReportedGuestPaymentClass.create
            |> List.append [organizerPaymentClass |> ReportedOrganizerPaymentClass.create]
            |> fun items -> { Items = items }

    type TotalPaymentAmountValue =
        ReportedPaymentClassList
            -> PositiveAmount

    let totalPaymentAmountValue : TotalPaymentAmountValue =
        fun paymentClassList ->
            paymentClassList.Items
            |> Seq.map (fun x -> x |> ReportedPaymentClass.classPaymentAmountValue)
            |> Seq.sum

    type FilterByPaymentType =
        PrescribedPaymentType
            -> ReportedPaymentClassList
            -> ReportedPaymentClassList

    let filterByPaymentType : FilterByPaymentType =
        fun paymentType paymentClassList ->
            paymentClassList.Items
            |> List.filter (fun x -> x |> ReportedPaymentClass.conditionPaymentType paymentType)
            |> fun items -> { Items = items }

    type MemberCount =
        ReportedPaymentClassList
            -> uint32

    let memberCount : MemberCount =
        fun paymentClassList ->
            paymentClassList.Items
            |> Seq.map ReportedPaymentClass.memberCount
            |> Seq.sum
