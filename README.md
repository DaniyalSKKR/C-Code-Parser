# Simple C Compiler
## Overview

This program is a simple C compiler written in F#. It consists of several components including a lexer, parser, and main compiler function. The compiler takes a "simple C" program as input, checks it for syntax errors, and produces an output indicating whether the program is valid or not.

### Features

  Lexer: Tokenizes the input program, identifying keywords, identifiers, literals, and special symbols.
  Parser: Parses the token stream to determine if the input program adheres to the syntax rules of simple C.
  Main Compiler: Orchestrates the lexer and parser, reading the input program from a file specified by the user and producing a compilation result.

### Usage

  Input Program: Write your simple C program in a file with the .c extension.

```
void main()
{
    int x     // syntax error: expecting ;, but found identifier:x

    x = 0;
    cout << x;
    cout << endl;
}
```
### Run the Compiler: Execute the F# program Compiler.fsx using an F# compiler or interpreter.

```bash

fsharpc Compiler.fsx
mono Compiler.exe
```
Enter Filename: When prompted, enter the filename of the simple C program.
```
    simpleC filename> program.c

    Compilation Result: The compiler will output whether the input program is valid (success) or contains syntax errors (syntax_error: ...).
```
### Example

Suppose we have a simple C program program.c as shown above. After running the compiler and providing the filename, the output might look like this:

```css

simpleC filename> program.c

compiling program.c...

["void"; "main"; "("; ")"; "{"; "int"; "identifier:x"; ...]

syntax_error: expecting ;, but found identifier:x
```
### Contributions

Contributions and improvements to this compiler are welcome. Feel free to fork the repository, make changes, and submit pull requests.

### Disclaimer

This program may not handle all possible cases or edge conditions found in real-world C programming. Use at your own risk.
