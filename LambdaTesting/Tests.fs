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

    

    

    [<Fact>]
    let ``VarParser should parse single variables`` () =
        let input = "x"
        let expected = Var 'x'
        match run varParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``FunctionParser should parse lambda functions`` () =
        let input = "λx.x"
        let expected = Function('x', Var 'x')
        match run functionParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)
    
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

    [<Fact>]
    let ``exprParser should parse single variables`` () =
        let input = "x"
        let expected = Var 'x'
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse lambda functions`` () =
        let input = "λx.x"
        let expected = Function('x', Var 'x')
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse two variables`` () =
        let input = "(xy)"
        let expected = Application(Var 'x', Var 'y')
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse one function and one variables`` () =
        let input = "(λx.xy)"
        let expected = Application(Function('x', Var 'x'), Var 'y')
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse two functions without brackets and ws `` () =
        let input = "(λx.xλy.y)"
        let expected = Application(Function('x', Var 'x'), Function('y', Var 'y'))
        match run exprParser input with
        | Success(result, _, _) ->
            printfn "Parsed result: %A" result
            Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse two functions with brackets and ws `` () =
        let input = "(λx.x λy.y)"
        let expected = Application(Function('x', Var 'x'), Function('y', Var 'y'))
        match run exprParser input with
        | Success(result, _, _) ->
            printfn "Parsed result: %A" result
            Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)





