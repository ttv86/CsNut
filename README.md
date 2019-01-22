# CsNut
C# to Squirrel compiler. Designed for OpenTTD AI development.

This program can be used to compile C#-source to Squirrel-language that is used to develop OpenTTD AIs.

Supported C# features:
  * Properties
  * Try-catch-finally
  * Using-statements
  * Constructor overloading
  * Namespaces
  * Enums
  * Lambdas
  * Nested methods

Unsupported C# features:
  * Async/await
  * Lock
  * Destructors
  * Exception filters
  * Goto

Supported FCL functions and classes:
  * System.Math: Abs, Ceiling, E, Floor, Max, Min, PI, Round, Sign
  * System.String: IsNullOrEmpty, Join, ToLower, ToUpper
  * System.Collections.Generic.List
  * System.Collections.Generic.Dictionary
  * System.Collections.Generic.HashSet