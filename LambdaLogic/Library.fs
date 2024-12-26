namespace LambdaLogic

open Parser

module Logic =
    let processInput (input: string) =
        Parser.parseLambda(input)
   