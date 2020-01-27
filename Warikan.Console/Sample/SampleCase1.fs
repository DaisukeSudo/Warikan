module Warikan.Console.Sample.SampleCase1

open Warikan.Domain.Common
open Warikan.Domain.DrinkingParty
open Warikan.Domain.SplitBillReport
open Warikan.Console.Baker.Domain.Accountant


let createDrinkingPartyId =
    fun () ->
        System.Guid.NewGuid()
        |> DrinkingPartyId.create

let createTotalBilledAmount =
    fun v ->
        v
        |> PositiveAmount.create
        |> TotalBilledAmount.create

let createPaymentClassId =
    fun id ->
        id
        |> PrescribedPaymentClassId.create

let createPaymentClass =
    fun id t v ->
        {
            PaymentClassId  =
                id
                |> createPaymentClassId
            PaymentType     =
                match t with
                | t when t = 2  -> PrescribedPaymentType.JUST_OR_MORE
                | _             -> PrescribedPaymentType.JUST
            PaymentAmount   =
                v
                |> PositiveAmount.create
                |> PrescribedPaymentAmount.create
        }

let creaetOrganizer =
    fun id ->
        id
        |> createPaymentClassId
        |> Organizer.create
    
let createGuestGroup =
    fun id n ->
        id
        |> createPaymentClassId
        |> GuestGroup.create (GuestsCount.create n)

let createDrinkingParty =
    fun () ->
        {
            DrinkingPartyId     = createDrinkingPartyId ()
            TotalBilledAmount   = createTotalBilledAmount 25000m
            Organizer           = creaetOrganizer 1u
            GuestGroupList      = GuestGroupList.create
                [
                    createGuestGroup 2u 2u
                    createGuestGroup 3u 3u
                ]
            PaymentClassList    = PrescribedPaymentClassList.create
                [
                    createPaymentClass 1u 2 0m
                    createPaymentClass 2u 1 3000m
                    createPaymentClass 3u 1 5000m
                ]
        }

type Run =
    unit -> unit

let run : Run =
    fun () ->
        createDrinkingParty ()
        |> AccountantBaker.calculate
        |> Warikan.Console.Serializer.serialize
        |> printfn "%s"
        ()
