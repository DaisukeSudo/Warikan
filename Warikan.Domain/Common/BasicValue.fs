namespace Warikan.Domain.Common

type PositiveAmount =
    private PositiveAmount of decimal
    with
    static member Zero = PositiveAmount 0M
    static member (+) (PositiveAmount a, PositiveAmount b) = PositiveAmount (a + b)

module PositiveAmount =
    let create v =
        if v < 0M then failwith "Please enter a positive number."
        else PositiveAmount v
    let value (PositiveAmount v) = v


type NegativeAmount =
    private NegativeAmount of decimal
        
module NegativeAmount =
    let create v =
        if v >= 0M then failwith "Please enter a negative number."
        else NegativeAmount v
    let value (NegativeAmount v) = v
