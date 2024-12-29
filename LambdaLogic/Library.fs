   namespace LambdaLogic

   open Parser
   open System.Text.Json
   open System.Text.Json.Serialization
   open FSharp.SystemTextJson
   open AST


    
   type Response = {lambdaInputString: Expr}
   module Logic =
       let processInput (input: string) =
           match Parser.parseLambda(input) with
           | Result.Ok(result) ->
               let options = JsonSerializerOptions(WriteIndented = true)
               options.Converters.Add(JsonFSharpConverter())
               let jsonResult = JsonSerializer.Serialize(result, options)
               jsonResult
           | Result.Error(errorMsg) -> "Invalid input: " + errorMsg.ToString()
   