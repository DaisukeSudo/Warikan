# API

- createDrinkingParty [POST]
  - IN
    - TotalBilledAmount
    - OrganizerPaymentType &lt;choise&gt;
      - | ZeroBurden
      - | RemainingBurden
      - | PaymentClassId
    - GuestGroupList
      - Items &lt;list&gt;
        - PaymentClassId
        - GuestsNumber
    - PaymentClassList
      - Items &lt;list&gt;
        - PaymentClassId
        - PaymentAmount
        - PaymentType
  - OUT
    - DrinkingPartyId

- setTotalBilledAmount [PATCH]
  - IN
    - DrinkingPartyId
    - TotalBilledAmount

- setOrganizerPaymentType [PATCH]
  - IN
    - DrinkingPartyID
    - OrganizerPaymentType &lt;choise&gt;
      - | ZeroBurden
      - | RemainingBurden
      - | PaymentClassId

- addPaymentClass [POST]
  - IN
    - DrinkingPartyId
    - PaymentType
    - PaymentAmount
    - GuestsNumber
  - OUT
    - PaymentClassId

- setPaymentClassPaymentAmount [PATCH]
  - IN
    - DrinkingPartyId
    - PaymentClassId
    - PaymentType
    - PaymentAmount

- setPaymentClassGuestsNumber [PATCH]
  - IN
    - DrinkingPartyId
    - PaymentClassId
    - GuestsNumber

- calculateSplitBillAmountById [GET]
  - IN
    - DrinkingPartyID
  - OUT
    - TotalBilledAmount
    - OrganizerPaymentAmount
    - PaymentClassList
      - Items &lt;list&gt;
        - PaymentClassID
        - PaymentAmount
        - IndividualPaymentAmount
        - GuestsNumber
    - ExtraOrShortage
