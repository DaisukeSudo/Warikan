namespace Warikan.Domain.SplitBillReport

type AdditionalPayersCount =
    private AdditionalPayersCount of uint32

module AdditionalPayersCount =
    let create v = AdditionalPayersCount v
    let value (AdditionalPayersCount v) = v
