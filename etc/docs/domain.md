# Domain Model

``` uml
@startuml
package "Domain" as domain {

  class DrinkingParty {
    DrinkingPartyID
    TotalBilledAmount
    Organizer
    GuestGroups : GuestGroup list
    PaymentClasses : PrescribedPaymentClass list
  }

  class DrinkingPartyID {
    Value : UUID
  }

  class TotalBilledAmount {
    Value : Decimal
  }

  class Organizer {
    PrescribedPaymentClass
  }

  class GuestGroup {
    PrescribedPaymentClass
    GuestsNumber
  }

  class GuestsNumber {
    Value : uint
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
    Value : Decimal
  }

  enum PrescribedPaymentType {
    JUST
    JUST_OR_MORE
  }


  class Accountant {
    calculate(DrinkingParty drinkingParty) : SplitBillReport
  }


  class SplitBillReport {
    TotalBilledAmount
    OrganizerPaymentAmount : IndividualPaymentAmount
    PaymentClasses : ReportedPaymentClass list
    ExcessOrDeficiency
  }

  class ReportedPaymentClass {
    PrescribedPaymentClassID
    PrescribedPaymentAmount
    IndividualPaymentAmount
    GuestsNumber
  }

  class IndividualPaymentAmount {
    Value : Decimal
  }

  class ExcessOrDeficiency {
    Value : Decimal
  }
}

' Domain
DrinkingParty *-d-> DrinkingPartyID
DrinkingParty *-d-> TotalBilledAmount
DrinkingParty *-d-> Organizer
DrinkingParty *-d-> GuestGroup
DrinkingParty *-d-> PrescribedPaymentClass
PrescribedPaymentClass *-d-> PrescribedPaymentClassID
PrescribedPaymentClass *-d-> PrescribedPaymentAmount
PrescribedPaymentClass *-d-> PrescribedPaymentType
Organizer *-d-> PrescribedPaymentClass
GuestGroup *-d-> PrescribedPaymentClass
GuestGroup *-d-> GuestsNumber

Accountant ..> DrinkingParty
Accountant ..> SplitBillReport

SplitBillReport  *-d-> TotalBilledAmount
SplitBillReport  *-d-> IndividualPaymentAmount
SplitBillReport *--d-> ReportedPaymentClass
SplitBillReport  *-d-> ExcessOrDeficiency
ReportedPaymentClass *-d-> PrescribedPaymentClassID
ReportedPaymentClass *-d-> PrescribedPaymentAmount
ReportedPaymentClass *-d-> IndividualPaymentAmount
ReportedPaymentClass *-d-> GuestsNumber

@enduml
