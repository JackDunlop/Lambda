namespace LambdaLogic

open Parser

module Logic =
    let processInput (input: string) =
        match Parser.parseLambda(input) with
        | Result.Ok(result) -> "Success: " + result.ToString()
        | Result.Error(errorMsg) -> "Invalid input: " + errorMsg