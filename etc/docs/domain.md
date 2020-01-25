# Domain Model

``` uml
@startuml
package "Domain" as domain {

  package DrinkingParty as pkg_drinking_party {

    class DrinkingParty {
      DrinkingPartyID
      TotalBilledAmount
      Organizer
      GuestGroupList
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
      GuestsCount
    }

    class GuestsCount {
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

    class Accountant << (F, #a8e3f1) >> {
      Calculate : DrinkingParty -> SplitBillReport
    }
  }

  package SplitBillReport as pkg_split_bill_report {

    class SplitBillReport {
      DrinkingPartyID
      TotalBilledAmount
      OrganizerPaymentClass
      GuestPaymentClassList
      TotalPaymentAmount
      ExtraOrShortage
    }

    class OrganizerPaymentClass {
      PrescribedPaymentClassID
      PrescribedPaymentAmount
      PrescribedPaymentType
      OrganizerPaymentAmount
    }

    class OrganizerPaymentAmount {
      Value : uint
    }

    class GuestPaymentClassList {
      Items : GuestPaymentClass list
    }

    class GuestPaymentClass {
      PrescribedPaymentClassID
      PrescribedPaymentAmount
      PrescribedPaymentType
      GuestPaymentAmount
      GuestsCount
    }

    class GuestPaymentAmount {
      Value : uint
    }

    class TotalPaymentAmount {
      Value : uint
    }

    class ExtraOrShortage << (U, #d9c383) >> {
      Extra of Extra
      Shortage of Shortage
    }

    class Extra {
      Value : uint
    }

    class Shortage {
      Value : int
    }
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
PrescribedPaymentClass *-d-> PrescribedPaymentClassID
PrescribedPaymentClass *-d-> PrescribedPaymentAmount
PrescribedPaymentClass *-d-> PrescribedPaymentType
Organizer *-d-> PrescribedPaymentClassID
Organizer -r[hidden]- GuestGroup
GuestGroup *-d-> PrescribedPaymentClassID
GuestGroup *-d-> GuestsCount

SplitBillReport *-d-> DrinkingPartyID
SplitBillReport *--d-> TotalBilledAmount
SplitBillReport *--d-> OrganizerPaymentClass
SplitBillReport *-d-> GuestPaymentClassList
SplitBillReport *--d-> TotalPaymentAmount
SplitBillReport *--d-> ExtraOrShortage
' OrganizerPaymentClass *-d-> PrescribedPaymentClassID
' OrganizerPaymentClass *-d-> PrescribedPaymentAmount
' OrganizerPaymentClass *-d-> PrescribedPaymentType
OrganizerPaymentClass *-d-> OrganizerPaymentAmount
GuestPaymentClassList *-d-> GuestPaymentClass
' GuestPaymentClass *-d-> PrescribedPaymentClassID
' GuestPaymentClass *-d-> PrescribedPaymentAmount
' GuestPaymentClass *-d-> PrescribedPaymentType
GuestPaymentClass *-d-> GuestsCount
GuestPaymentClass *-d-> GuestPaymentAmount
ExtraOrShortage *-d-> Extra
ExtraOrShortage *-d-> Shortage

@enduml
