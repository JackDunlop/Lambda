namespace LambdaTesting
module Tests =
    open System
    open Xunit
    open Parser
    open AST
    open LambdaLogic
    open FParsec

    [<Fact>]
    let ``My test`` () =
        Assert.True(true)

    
    let runParser parser input =
        match run parser input with
        | Success(result, _, _) ->
            printfn "Successfully parsed: %A" result
            Some(result)
        | Failure(errorMsg, _, _) ->
            printfn "Parsing failed with error: %s" errorMsg
            None

    [<Fact>]
    let ``VarParser should parse single variables`` () =
        let result = runParser varParser "x"
        match result with
        | Some(Var 'x') -> Assert.True(true)
        | _ -> Assert.True(false, "Failed to parse variable")

    [<Fact>]
    let ``FunctionParser should parse lambda functions`` () =
        let result = runParser functionParser "λx.x"
        match result with
        | Some(Function('x', Var 'x')) -> Assert.True(true)
        | _ -> Assert.True(false, "Failed to parse function")
    
    [<Fact>]
    let ``applicationParser should parse two variables`` () =
        let input = "(xy)"
        let expected = Application(Var 'x', Var 'y')
        match run applicationParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``applicationParser should parse one function and one variables`` () =
        let input = "(λx.xy)"
        let expected = Application(Function('x', Var 'x'), Var 'y')
        match run applicationParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``applicationParser should parse two functions without brackets and ws `` () =
        let input = "(λx.xλy.y)"
        let expected = Application(Function('x', Var 'x'), Function('y', Var 'y'))
        match run applicationParser input with
        | Success(result, _, _) ->
            printfn "Parsed result: %A" result
            Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``applicationParser should parse two functions with brackets and ws `` () =
        let input = "(λx.x λy.y)"
        let expected = Application(Function('x', Var 'x'), Function('y', Var 'y'))
        match run applicationParser input with
        | Success(result, _, _) ->
            printfn "Parsed result: %A" result
            Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)


