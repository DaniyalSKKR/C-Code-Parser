//
// Parser for simple C programs.  This component checks 
// the input program to see if it meets the syntax rules
// of simple C.  The parser returns a string denoting
// success or failure. 
//
// Returns: the string "success" if the input program is
// legal, otherwise the string "syntax_error: ..." is
// returned denoting an invalid simple C program.
//
// Daniyal Khokhar
//
// Original author:
//   Prof. Joe Hummel
//   U. of Illinois, Chicago
//   CS 341, Spring 2022
//

namespace compiler

module parser =
  //
  // NOTE: all functions in the module must be indented.
  //

  let beginsWith (pattern:string) (token:string) = 
    token.StartsWith(pattern)

  //
  // matchToken
  //
  let private matchToken expected_token (tokens: string list) =
    //
    // if the next token matches the expected token,  
    // keep parsing by returning the rest of the tokens.
    // Otherwise throw an exception because there's a 
    // syntax error, effectively stopping compilation
    // at the first error.
    //
    let next_token = List.head tokens

    //next_token.StartsWith(expected_token)
    if (beginsWith expected_token next_token) then  
      List.tail tokens
    else
      failwith ("expecting " + expected_token + ", but found " + next_token)
  //
  let rec private empty tokens =
    matchToken ";" tokens;
  
  //
  let rec private vardecl tokens = 
    let next_token = List.head tokens
    let tail = List.tail tokens

    if (beginsWith "identifier:" next_token) then
      empty tail
        
    else
      failwith ("expecting identifier or literal, but found " + next_token)
  //
  let rec private input tokens = 
    let temp = matchToken ">>" tokens
    let next_token = List.head temp
    let tail = List.tail temp
    
    if (beginsWith "identifier:" next_token) then
      empty tail
    else
      failwith ("expecting identifier, but found " + next_token)
  //
  
  
  let private expr_value tokens =
    let next_token = List.head tokens
    if (beginsWith "identifier:" next_token) then
      List.tail tokens
    elif (beginsWith "int_literal:" next_token) then
      List.tail tokens
    elif (beginsWith "str_literal:" next_token) then
      List.tail tokens
    elif (beginsWith "true" next_token) then
      List.tail tokens
    elif (beginsWith "false" next_token) then
      List.tail tokens
    else 
      failwith ("expecting identifier or literal, but found " + next_token)
 //

  
    
  let private expr_op tokens =
    let next_token = List.head tokens
    
    match next_token with
    | "+"  -> List.tail tokens
    | "-" -> List.tail tokens
    | "*" -> List.tail tokens
    | "/" -> List.tail tokens
    | "^" -> List.tail tokens
    | "<" -> List.tail tokens
    | "<=" -> List.tail tokens
    | ">" -> List.tail tokens
    | ">=" -> List.tail tokens
    | "==" -> List.tail tokens
    | "!=" -> List.tail tokens
    | _ -> failwith ("expecting ;, but found " + next_token)

  //
  let private expr tokens =
    let w = expr_value tokens //var123
    if (List.head w) = ";" then
      empty w
    elif (List.head w) = ")" then
      w
    else 
      let x = expr_op w
      let y = expr_value x
      y

  //
  let private output_value tokens = 
    if (List.head tokens) = "endl" then
      matchToken "endl" tokens
    else
      expr_value tokens
  //
  let private output tokens =
    let temp = matchToken "<<" tokens
    // let next_token = List.head temp
    // let tail = List.tail temp

    let temp2 = output_value temp
    empty temp2
  //
  let rec private assignment tokens = 
    let temp = matchToken "=" tokens
    expr temp
    
  //
  let private conditions tokens =
    expr tokens
  //MORE STATEMENTS

  let rec private morestmts tokens =
    if (List.head tokens) = "}" then
      tokens
    else
      stmt tokens
    
  and private stmt tokens =
    let next_token = List.head tokens
    
    if next_token = "int" then
      morestmts (vardecl (List.tail tokens))
      
    elif next_token = "cin" then
      morestmts (input (List.tail tokens))

    elif next_token = "cout" then
      morestmts (output (List.tail tokens))
      
    elif (beginsWith "identifier" next_token) then
      morestmts (assignment (List.tail tokens))
      
    elif next_token = "if" then
      ifstmt (List.tail tokens)
      
    elif next_token = ";" then
      let temp = empty tokens
      morestmts temp
    elif next_token = "else" then //added
      tokens
    else 
      failwith ("expecting statement, but found " + next_token)
    // else 
    //   if next_token = ";" then
    //     let tokenz = empty tokens
    //     morestmts tokenz
    //   else 
    //     failwith ("expecting statement, but found " + next_token)
        
      
  and private then_part tokens =
    stmt tokens

  and private else_part tokens = 
    if (List.head tokens) = "}" then
      tokens
    else
      let temp = matchToken "else" tokens
      stmt temp

  and private ifstmt tokens =
  // if ( <condition> ) <then-part> <else-part>
    let temp1 = matchToken "(" tokens
    let temp2 = conditions temp1
    let temp3 = matchToken ")" temp2
    let temp4 = then_part temp3
    let temp5 = else_part temp4
    
    temp5
  
  let rec private stmts tokens = 
    let tokens2 = stmt tokens
    tokens2
    // match list with
    // | [] -> 
    // | head::tail -> stmts tail
    // | _ -> tokens
      

  



  //end of added functions^
  

  //
  // simpleC
  //
  let private simpleC tokens = 
    //matchToken "$" tokens
    let t2 = matchToken "void" tokens
    let t3 = matchToken "main" t2
    let t4 = matchToken "(" t3
    let t5 = matchToken ")" t4
    let t6 = matchToken "{" t5
    let t7 = stmts t6
    let t8 = matchToken "}" t7
    let t9 = matchToken "$" t8
    t9

  //
  // parse tokens
  //
  // Given a list of tokens, parses the list and determines
  // if the list represents a valid simple C program.  Returns
  // the string "success" if valid, otherwise returns a 
  // string of the form "syntax_error:...".
  //
  let parse tokens = 
    try
      let result = simpleC tokens
      "success"
    with 
      | ex -> "syntax_error: " + ex.Message
