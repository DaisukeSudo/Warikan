# Domain Model

``` uml
@startuml
package "Domain" as domain {

  package Common as pkg_common {

    ' BasicValue

    class PositiveAmount {
      of decimal
    }

    class NegativeAmount {
      of decimal
    }
  }

  package DrinkingParty as pkg_drinking_party {

    ' PrescribedPaymentClass

    class PrescribedPaymentClassId {
      of uint
    }

    enum PrescribedPaymentType {
      JUST
      JUST_OR_MORE
    }

    class PrescribedPaymentAmount {
      of PositiveAmount
    }

    class PrescribedPaymentClass {
      PrescribedPaymentClassId
      PrescribedPaymentAmount
      PrescribedPaymentType
    }

    class PrescribedPaymentClassList {
      Items : PrescribedPaymentClass list
      Add()
      FindOneById()
    }

    ' GuestsCount

    class GuestsCount {
      of uint
    }

    class GuestGroup {
      PaymentClassId : PrescribedPaymentClassId
      GuestsCount
    }

    class GuestGroupList {
      Items : GuestGroup list
    }

    ' Organizer

    class Organizer {
      PaymentClassId : PrescribedPaymentClassId
    }

    ' TotalBilledAmount

    class TotalBilledAmount {
      of PositiveAmount
    }

    ' DrinkingParty

    class DrinkingPartyID {
      of UUID
    }

    class DrinkingParty {
      DrinkingPartyID
      TotalBilledAmount
      Organizer
      GuestGroupList
      PaymentClassList : PrescribedPaymentClassList
    }
  }

  package Accountant as pkg_accountant {

    class Accountant << (F, #a8e3f1) >> {
      Calculate : DrinkingParty -> SplitBillReport
    }
  }

  package SplitBillReport as pkg_split_bill_report {

    ' OrganizerPaymentClass

    class OrganizerPaymentAmount {
      of PositiveAmount
    }

    class OrganizerPaymentClass {
      PrescribedPaymentClassId
      PrescribedPaymentAmount
      PrescribedPaymentType
      OrganizerPaymentAmount
      CreateBy()
      ClassPaymentAmountValue()
    }

    ' GuestPaymentClass

    class GuestPaymentAmount {
      of PositiveAmount
    }

    class GuestPaymentClass {
      PrescribedPaymentClassId
      PrescribedPaymentAmount
      PrescribedPaymentType
      GuestPaymentAmount
      GuestsCount
      CreateBy()
      ClassPaymentAmountValue()
    }

    class GuestPaymentClassList {
      Items : GuestPaymentClass list
      CreateBy()
    }

    '' ReportedPaymentClass
    '
    'class ReportedPaymentClass << (U, #d9c383) >> {
    '  ReportedOrganizerPaymentClass of OrganizerPaymentClass
    '  ReportedGuestPaymentClass     of GuestPaymentClass
    '}
    '
    'class ReportedPaymentClassList {
    '  Items : ReportedPaymentClass list
    '  TotalPaymentAmountValue()
    '}

    ' TotalPaymentAmount

    class TotalPaymentAmount {
      of PositiveAmount
      CreateBy()
    }

    ' ExtraOrShortage

    class ExtraOrShortage << (U, #d9c383) >> {
      Extra    of PositiveAmount
      Shortage of NegativeAmount
      CreateBy()
    }

    ' SplitBillReport

    class SplitBillReport {
      DrinkingPartyID
      TotalBilledAmount
      OrganizerPaymentClass
      GuestPaymentClassList
      TotalPaymentAmount
      ExtraOrShortage
    }

    '' UnadjustedSplitBillReport
    '
    'class UnadjustedSplitBillReport {
    '  DrinkingPartyID
    '  TotalBilledAmount
    '  OrganizerPaymentClass
    '  GuestPaymentClassList
    '  TotalPaymentAmount
    '  ExtraOrShortage
    '  CreateBy()
    '}
  }
}

Accountant ..> DrinkingParty
Accountant ..> SplitBillReport
DrinkingParty --r[hidden]-- SplitBillReport

DrinkingParty *-d-> DrinkingPartyID
DrinkingParty *--d-> TotalBilledAmount
DrinkingParty *-d-> Organizer
DrinkingParty *-d-> GuestGroupList
DrinkingParty *-d-> PrescribedPaymentClassList
GuestGroupList *-d-> GuestGroup
PrescribedPaymentClassList *-d-> PrescribedPaymentClass
PrescribedPaymentClass *-d-> PrescribedPaymentClassId
PrescribedPaymentClass *-d-> PrescribedPaymentAmount
PrescribedPaymentClass *-d-> PrescribedPaymentType
Organizer *-d-> PrescribedPaymentClassId
Organizer -r[hidden]- GuestGroup
GuestGroup *-d-> PrescribedPaymentClassId
GuestGroup *-d-> GuestsCount

SplitBillReport *-d-> DrinkingPartyID
SplitBillReport *--d-> TotalBilledAmount
SplitBillReport *--d-> OrganizerPaymentClass
SplitBillReport *-d-> GuestPaymentClassList
SplitBillReport *--d-> TotalPaymentAmount
SplitBillReport *--d-> ExtraOrShortage
' OrganizerPaymentClass *-d-> PrescribedPaymentClassId
' OrganizerPaymentClass *-d-> PrescribedPaymentAmount
' OrganizerPaymentClass *-d-> PrescribedPaymentType
OrganizerPaymentClass *-d-> OrganizerPaymentAmount
GuestPaymentClassList *-d-> GuestPaymentClass
' GuestPaymentClass *-d-> PrescribedPaymentClassId
' GuestPaymentClass *-d-> PrescribedPaymentAmount
' GuestPaymentClass *-d-> PrescribedPaymentType
GuestPaymentClass *-d-> GuestsCount
GuestPaymentClass *-d-> GuestPaymentAmount

@enduml
