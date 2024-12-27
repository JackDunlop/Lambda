module Parser
open FParsec
open AST
// no numbers
// lots of brackets - recursion

//<λexp>::= <var>
//        | λ<var>.<λexp>
//        | (<λexp> <λexp>)

// Todo:
// Probably should deal with white space instead of just everything next to eachother (λx.λy.(xy)) -> (λ x. λ y. (x y))


let ws = spaces

let keyword (str:string) : Parser<string,unit> = 
    pstringCI str 

let lambdaParser = 
    keyword "λ"

let charParser: Parser<char, unit> =
    satisfy isAsciiLetter

let varParser: Parser<Expr, unit> =
    charParser |>> Var


let exprParser, exprParserRef = createParserForwardedToRef<Expr, unit>()

let applicationParser : Parser<Expr, unit> =
    keyword "(" >>. exprParser .>> opt ws .>>. exprParser .>> keyword ")" 
    |>> (fun (expr1, expr2) -> Application(expr1, expr2))

let functionParser : Parser<Expr, unit> =  
    (lambdaParser >>. charParser .>> keyword ".") .>>. exprParser
    |>> (fun (param, body) -> Function(param, body))

exprParserRef := 
    functionParser
    <|> varParser
    <|> applicationParser 


let parseLambda (expr: string) : Result<Expr, string> =
    match run exprParser expr with   
    | Success(result, _, _) -> Result.Ok(result)
    | Failure(errorMsg, _, _) -> Result.Error (handleSyntaxError errorMsg)