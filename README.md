# Lambda

A super basic Lambda calculus interpreter in F#.

#### AST
- [x] AST

    ```plaintext
    <expression> := <var> | <function> | <application>
    <function> := <name> <expression>
    <application> := <expression> <expression>
    ```

#### Parser
- [x] Var Parser
- [x] Function Parser
- [x] Application Parser
- [x] Expression Parser

    ```plaintext
    <λexp> ::= <var>
             | λ<var>.<λexp>
             | (<λexp> <λexp>)
    ```

#### Transformations
- [ ] Alpha-reduction
- [ ] Beta-conversion
