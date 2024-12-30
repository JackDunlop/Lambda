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
        let expected = Function ('x', Application (Var 'x', Var 'y'))
        match run applicationParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``applicationParser should parse two functions without brackets and ws `` () =
        let input = "(λx.xλy.y)"
        let expected = Function ('x', Application (Var 'x', Function ('y', Var 'y')))
        match run applicationParser input with
        | Success(result, _, _) ->
            printfn "Parsed result: %A" result
            Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``applicationParser should parse two functions with brackets and ws `` () =
        let input = "(λx.x λy.y)"
        let expected = Function ('x', Application (Var 'x', Function ('y', Var 'y')))
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
        let expected = Function ('x', Application (Var 'x', Var 'y'))
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse two functions without brackets and ws `` () =
        let input = "(λx.xλy.y)"
        let expected = Function ('x', Application (Var 'x', Function ('y', Var 'y')))
        match run exprParser input with
        | Success(result, _, _) ->
            printfn "Parsed result: %A" result
            Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse two functions with brackets and ws `` () =
        let input = "(λx.x λy.y)"
        let expected =  Function ('x', Application (Var 'x', Function ('y', Var 'y')))
        match run exprParser input with
        | Success(result, _, _) ->
            printfn "Parsed result: %A" result
            Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse complex expression ((λx.u λu.λu.u) λx.((x x) λy.y))`` () =
        let input = "((λx.u λu.λu.u) λx.((x x) λy.y))"
        let expected =
            Application(Function('x', Application (Var 'u', Function ('u', Function ('u', Var 'u')))),Function('x', Application (Application (Var 'x', Var 'x'), Function ('y', Var 'y'))))
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse nested lambda expressions (λx.(λy.(x y)) (λz.z))`` () =
        let input = "λx.(λy.(x y)) (λz.z)"
        let expected =
            Function('x',
                Application(
                    Function('y', Application(Var 'x', Var 'y')),
                    Function('z', Var 'z')
                )
            )
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse self-application ((λx.(x x)) (λx.(x x)))`` () =
        let input = "((λx.(x x)) (λx.(x x)))"
        let expected =
            Application(
                Function('x', Application(Var 'x', Var 'x')),
                Function('x', Application(Var 'x', Var 'x'))
            )
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse multiple nested applications (λa.(λb.(a b)) (λc.(c c)))`` () =
        let input = "λa.(λb.(a b)) (λc.(c c))"
        let expected =
            Function('a',
                Application(
                    Function('b', Application(Var 'a', Var 'b')),
                    Function('c', Application(Var 'c', Var 'c'))
                )
            )
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse very nested applications (λa.(λb.(λc.(λd.(a (b (c d)))))))`` () =
        let input = "λa.(λb.(λc.(λd.(a (b (c d))))))"
        let expected =
            Function('a',
                Function('b',
                    Function('c',
                        Function('d',
                            Application(
                                Var 'a',
                                Application(
                                    Var 'b',
                                    Application(
                                        Var 'c',
                                        Var 'd'
                                    )
                                )
                            )
                        )
                    )
                )
            )
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse double application with nested lambdas ( (λx.x) (λy.(y y)) )`` () =
        let input = "((λx.x) (λy.(y y)))"
        let expected =
            Application(
                Function('x', Var 'x'),
                Function('y', Application(Var 'y', Var 'y'))
            )
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse triple nested application ( ((λx.(x x)) (λy.(y y))) (λz.z) )`` () =
        let input = "(((λx.(x x)) (λy.(y y))) (λz.z))"
        let expected =
            Application(
                Application(
                    Function('x', Application(Var 'x', Var 'x')),
                    Function('y', Application(Var 'y', Var 'y'))
                ),
                Function('z', Var 'z')
            )
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse shadowed variables (λa.(λa.(λa.a)))`` () =
        let input = "λa.(λa.(λa.a))"
        let expected =
            Function('a',
                Function('a',
                    Function('a', Var 'a')
                )
            )
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse lambda body with nested application (λa.(a (λb.(b b))))`` () =
        let input = "λa.(a (λb.(b b)))"
        let expected =
            Function('a',
                Application(
                    Var 'a',
                    Function('b', Application(Var 'b', Var 'b'))
                )
            )
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse multiple arg chain (λa.(λb.(λc.(a b c))))`` () =
        let input = "λa.(λb.(λc.(a b c)))"
        let expected =
            Function('a',
                Function('b',
                    Function('c',
                        Application(
                            Application(Var 'a', Var 'b'),
                            Var 'c'
                        )
                    )
                )
            )
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse very deep lambda nesting (λa.(λb.(λc.(λd.(λe.(a (b (c (d e)))))))))`` () =
        let input = "λa.(λb.(λc.(λd.(λe.(a (b (c (de))))))))"
        let expected =
            Function('a',
                Function('b',
                    Function('c',
                        Function('d',
                            Function('e',
                                Application(
                                    Var 'a',
                                    Application(
                                        Var 'b',
                                        Application(
                                            Var 'c',
                                            Application(Var 'd', Var 'e')
                                        )
                                    )
                                )
                            )
                        )
                    )
                )
            )
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)

    [<Fact>]
    let ``exprParser should parse complex bracket usage λx.((λy.(y x)) (λz.z))`` () =
        let input = "λx.((λy.(y x)) (λz.z))"
        let expected =
            Function('x',
                Application(
                    Function('y', Application(Var 'y', Var 'x')),
                    Function('z', Var 'z')
                )
            )
        match run exprParser input with
        | Success(result, _, _) -> Assert.Equal(expected, result)
        | Failure(errorMsg, _, _) -> Assert.True(false, errorMsg)




