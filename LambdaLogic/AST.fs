module AST



type Expr =  //<expression> :=   <var>  |   <function>  |   <application>
    | Var of char // <var>
    | Function of lhs:char * rhs:Expr //<function> := <name><expression>
    | Application of lhs:Expr * rhs:Expr //<application> := <expression><expression> 

let handleSyntaxError (message: string) : string =
    $"Syntax Error: {message}"

