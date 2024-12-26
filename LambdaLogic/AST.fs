module AST

// expr var

//<λexp>::= <var>
//        | λ<var> . <λexp>
//        | ( <λexp> <λexp> )

type Expr =  //<expression> :=   <var>  |   <function>  |   <application>
    | Var of char
    | Function of lhs:char * rhs:Expr //<function> := λ <name>.<expression>
    | Application of lhs:Expr * rhs:Expr //<application> := <expression><expression> 

let handleSyntaxError (message: string) : string =
    $"Syntax Error: {message}"