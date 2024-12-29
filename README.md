# Lambda

A super basic Lambda calculus interpreter in F#.

## Lambda Frontend
- [ ] Tree Visualisations

## Lambda Logic

### AST
- [x] AST

    ```plaintext
    <expression> := <var> | <function> | <application>
    <function> := <name> <expression>
    <application> := <expression> <expression>
    ```

### Parser
- [x] Var Parser
- [x] Function Parser
- [x] Application Parser
- [x] Expression Parser

    ```plaintext
    <λexp> ::= <var>
             | λ<var>.<λexp>
             | (<λexp> <λexp>)
    ```

### Transformations
- [ ] Alpha-reduction
- [ ] Beta-conversion



# Resoucres
https://opendsa.cs.vt.edu/ODSA/Books/PL/html/Syntax.html
https://homepage.cs.uiowa.edu/~slonnegr/plf/Book/Chapter5.pdf
CAB402 Lectures from QUT
