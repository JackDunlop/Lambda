namespace LambdaWebApp.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open LambdaLogic

[<ApiController>]
[<Route("[controller]")>]
type LambdaController (logger : ILogger<LambdaController>) =
    inherit ControllerBase()

    [<HttpGet(Name = "Lambda Input")>]
    member _.LambdaInput(input: string) : IActionResult =
        let result = Logic.processInput(input)
        let response = { error = false; data = result }
        base.Ok(result)

and internal Response = {
    error: bool
    data: string
}