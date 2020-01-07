namespace Warikan.Web.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Warikan.Web

[<ApiController>]
[<Route("[controller]")>]
type WarikanController (logger : ILogger<WarikanController>) =
    inherit ControllerBase()

    let summaries = [| "Freezing"; "Bracing"; "Chilly"; "Cool"; "Mild"; "Warm"; "Balmy"; "Hot"; "Sweltering"; "Scorching" |]

    [<HttpGet>]
    member _.Get() =
        let values = [|"Hello"; "World"; "First F#/ASP.NET Core web API!"|]
        ActionResult<string[]>(values)
