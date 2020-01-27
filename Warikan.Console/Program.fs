module Warikan.Console.Program

open System

[<EntryPoint>]
let main argv =
    Sample.SampleCase1.run ()
    0 // return an integer exit code
