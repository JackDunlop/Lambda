module Parser
open FParsec
open AST

//<λexp>::= <var>
//        | λ<var>.<λexp>
//        | (<λexp> <λexp>)


let exprParser, exprParserRef = createParserForwardedToRef<Expr, unit>()
let ws = spaces

let keyword (str:string) : Parser<string,unit> = 
    pstringCI str 

let lambdaParser = 
    keyword "λ"

let charParser: Parser<char, unit> =
    satisfy isAsciiLetter

let varParser: Parser<Expr, unit> =
    charParser |>> Var

let functionParser : Parser<Expr, unit> =  
    lambdaParser .>> ws >>. charParser .>> ws .>> keyword "." .>> ws .>>. exprParser
    |>> (fun (param, body) -> Function(param, body))

let applicationParser : Parser<Expr, unit> =
    between (keyword "(" .>> ws) (ws >>. keyword ")") (ws >>. exprParser)
    

let termParser: Parser<Expr, unit> =
    functionParser
    <|> varParser
    <|> applicationParser

exprParserRef :=
    pipe2 termParser (many (ws >>. termParser))
        (fun first rest -> List.fold (fun acc x -> Application(acc, x)) first rest)


let parseLambda (expr: string) : Result<Expr, string> =
    match run exprParser expr with   
    | Success(result, _, _) -> Result.Ok(result)
    | Failure(error, _, _) -> Result.Error(error.ToString())