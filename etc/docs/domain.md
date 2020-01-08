# Domain Model

``` uml
@startuml
package "Domain" as domain {

  class DrinkingParty {
    DrinkingPartyID
    TotalBilledAmount
    OrganizerPaymentClass : PrescribedPaymentClass
    PaymentClasses : PrescribedPaymentClass list
  }

  class DrinkingPartyID {
    Value : UUID
  }

  class TotalBilledAmount {
    Value : Decimal
  }

  class PrescribedPaymentClass {
    PrescribedPaymentClassID
    PrescribedPaymentAmount
    PrescribedPaymentType
    PaymentClassParticipantsNumber
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

  class PaymentClassParticipantsNumber {
    Value : uint
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
    PaymentClassParticipantsNumber
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
DrinkingParty *-d-> PrescribedPaymentClass
PrescribedPaymentClass *-d-> PrescribedPaymentClassID
PrescribedPaymentClass *-d-> PrescribedPaymentAmount
PrescribedPaymentClass *-d-> PrescribedPaymentType
PrescribedPaymentClass *-d-> PaymentClassParticipantsNumber

Accountant ..> DrinkingParty
Accountant ..> SplitBillReport

SplitBillReport *-d-> TotalBilledAmount
SplitBillReport *-d-> IndividualPaymentAmount
SplitBillReport *-d-> ReportedPaymentClass
SplitBillReport *-d-> ExcessOrDeficiency
ReportedPaymentClass *-d-> PrescribedPaymentClassID
ReportedPaymentClass *-d-> PrescribedPaymentAmount
ReportedPaymentClass *-d-> IndividualPaymentAmount
ReportedPaymentClass *-d-> PaymentClassParticipantsNumber

@enduml
