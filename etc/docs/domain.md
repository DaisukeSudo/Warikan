# Domain Model

``` uml
@startuml
package "Domain" as domain {

  package DrinkingParty as pkg_drinking_party {

    class DrinkingParty {
      DrinkingPartyID
      TotalBilledAmount
      Organizer
      GuestGroupList : GuestGroupList
      PaymentClassList : PrescribedPaymentClassList
    }

    class DrinkingPartyID {
      Value : UUID
    }

    class TotalBilledAmount {
      Value : uint
    }

    class Organizer {
      PrescribedPaymentClassID
    }

    class GuestGroupList {
      Items : GuestGroup list
    }

    class GuestGroup {
      PrescribedPaymentClassID
      GuestsNumber
    }

    class GuestsNumber {
      Value : uint
    }

    class PrescribedPaymentClassList {
      Items : PrescribedPaymentClass list
    }

    class PrescribedPaymentClass {
      PrescribedPaymentClassID
      PrescribedPaymentAmount
      PrescribedPaymentType
    }

    class PrescribedPaymentClassID {
      Value : uint
    }

    class PrescribedPaymentAmount {
      Value : uint
    }

    enum PrescribedPaymentType {
      JUST
      JUST_OR_MORE
    }
  }

  package Accountant as pkg_accountant {

    class "calculate" as Accountant_calculate << (F, #a8e3f1) >> {
      DrinkingParty -> SplitBillReport
    }
  }

  package SplitBillReport as pkg_split_bill_report {

    class SplitBillReport {
      TotalBilledAmount
      OrganizerPaymentAmount
      PaymentClassList : ReportedPaymentClassList
      ExtraOrShortage
    }

    class OrganizerPaymentAmount {
      Value : uint
    }

    class ReportedPaymentClassList {
      Items : ReportedPaymentClass list
    }

    class ReportedPaymentClass {
      PrescribedPaymentClassID
      PrescribedPaymentAmount
      GuestPaymentAmount
      GuestsNumber
    }

    class GuestPaymentAmount {
      Value : uint
    }

    class ExtraOrShortage {
      Value : int
    }
  }
}

Accountant_calculate ..> DrinkingParty
Accountant_calculate ..> SplitBillReport
DrinkingParty --r[hidden]-- SplitBillReport

DrinkingParty *-d-> DrinkingPartyID
DrinkingParty *-d-> TotalBilledAmount
DrinkingParty *-d-> Organizer
DrinkingParty *-d-> GuestGroupList
DrinkingParty *-d-> PrescribedPaymentClassList
GuestGroupList *-d-> GuestGroup
PrescribedPaymentClassList *-d-> PrescribedPaymentClass
PrescribedPaymentClass *-d-> PrescribedPaymentClassID
PrescribedPaymentClass *-d-> PrescribedPaymentAmount
PrescribedPaymentClass *-d-> PrescribedPaymentType
Organizer *-d-> PrescribedPaymentClassID
GuestGroup *-d-> PrescribedPaymentClassID
GuestGroup *-d-> GuestsNumber

SplitBillReport *-d-> TotalBilledAmount
SplitBillReport *-d-> OrganizerPaymentAmount
SplitBillReport *-d-> ReportedPaymentClassList
SplitBillReport *-d-> ExtraOrShortage
ReportedPaymentClassList *-d-> ReportedPaymentClass
ReportedPaymentClass *-d-> PrescribedPaymentClassID
ReportedPaymentClass *-d-> PrescribedPaymentAmount
ReportedPaymentClass *-d-> GuestPaymentAmount
ReportedPaymentClass *-d-> GuestsNumber

@enduml
