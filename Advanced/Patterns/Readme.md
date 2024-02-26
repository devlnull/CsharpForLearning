## Patterns

```csharp
if (obj is string { Length:4 })
 Console.WriteLine ("A string with 4 characters");
 ```

Patterns are supported in the following contexts:

- After the is operator (variable is pattern)
- In switch statements
- In switch expressions

### Constant Pattern
The constant pattern lets you match directly to a constant, and is useful when
working with the object type:
```csharp
obj is 3
```
This expression is equivalent to the following:
```csharp
obj is int && (int)obj == 3
```
(Being a static operator, C# won’t let you use == to compare an object directly to a
constant, because the compiler needs to know the types in advance.)
On its own, this pattern is only marginally useful in that there’s a reasonable
alternative:
```csharp
if (3.Equals (obj)) ...
```
As we’ll see soon, the constant pattern becomes much more useful with pattern combinators.

### Relational Patterns
From C# 9, you can use the <, >, <=, and >= operators in patterns:
```csharp
if (x is > 100) Console.WriteLine ("x is greater than 100");
```
This becomes meaningfully useful in a switch:
```csharp
string GetWeightCategory (decimal bmi) => bmi switch
{
< 18.5m => "underweight",
< 25m => "normal",
< 30m => "overweight",
_ => "obese"
};
```
Relational patterns become even more useful in conjunction with pattern combinators.

##### NOTE: The relational pattern also works when the variable has a compile-time type of object, but you have to be extremely careful with your use of numeric constants. In the following example, the last line prints False because we are attempting to match a decimal value to an integer literal:
```csharp
object obj = 2m; // obj is decimal
Console.WriteLine (obj is < 3m); // True
Console.WriteLine (obj is < 3); // False
```

### Pattern Combinators
```csharp
bool IsJanetOrJohn (string name) => name.ToUpper() is "JANET" or "JOHN";
bool IsVowel (char c) => c is 'a' or 'e' or 'i' or 'o' or 'u';
bool Between1And9 (int n) => n is >= 1 and <= 9;
bool IsLetter (char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';
```

A nice trick is to combine the not combinator with the type pattern to test whether an object is (not) a type:
```csharp
if (obj is not string) ...
This looks nicer than:
if (!(obj is string))
```

### var Pattern
The var pattern is a variation of the type pattern whereby you replace the type name
with the var keyword. The conversion always succeeds, so its purpose is merely to
let you reuse the variable that follows:
```csharp
bool IsJanetOrJohn (string name) => name.ToUpper() is var upper && (upper == "JANET" || upper == "JOHN")
```
This is equivalent to:
```csharp
bool IsJanetOrJohn (string name)
{
 string upper = name.ToUpper();
 return upper == "JANET" || upper == "JOHN";
}
```

The ability to introduce and reuse an intermediate variable (upper, in this case)
in an expression-bodied method is convenient—particularly in lambda expressions.
Unfortunately, it tends to be useful only when the method in question has a bool
return type.

### Tuple and Positional Patterns
The tuple pattern (introduced in C# 8) matches tuples:
```csharp
var p = (2, 3);
Console.WriteLine (p is (2, 3)); // True
```
You can use this to switch on multiple values:
```csharp
int AverageCelsiusTemperature (Season season, bool daytime) =>
 (season, daytime) switch
 {
 (Season.Spring, true) => 20,
 (Season.Spring, false) => 16,
 (Season.Summer, true) => 27,
 (Season.Summer, false) => 22,
 (Season.Fall, true) => 18,
 (Season.Fall, false) => 12,
 (Season.Winter, true) => 10,
 (Season.Winter, false) => -2,
 _ => throw new Exception ("Unexpected combination")
};
enum Season { Spring, Summer, Fall, Winter };
```

Here’s a switch expression that combines a type pattern with a positional pattern:
```csharp
string Print (object obj) => obj switch 
{
 Point (0, 0) => "Empty point",
 Point (var x, var y) when x == y => "Diagonal"
 ...
};
```

### Property Patterns
A property pattern (C# 8+) matches on one or more of an object’s property values.
We gave a simple example previously in the context of the is operator:
```csharp
if (obj is string { Length:4 }) ...
```
However, this doesn’t save much over the following:
```csharp
if (obj is string s && s.Length == 4) ...
```

With switch statements and expressions, property patterns are more useful. Consider the System.Uri class, which represents a URI. It has properties that include
Scheme, Host, Port, and IsLoopback. In writing a firewall, we could decide whether
to allow or block a URI by employing a switch expression that uses property
patterns:
```csharp
bool ShouldAllow (Uri uri) => uri switch
{
 { Scheme: "http", Port: 80 } => true,
 { Scheme: "https", Port: 443 } => true,
 { Scheme: "ftp", Port: 21 } => true,
 { IsLoopback: true } => true,
 _ => false
};
```
As you might expect with type patterns, you can introduce a variable at the end of a
clause and then consume that variable:
```csharp
bool ShouldAllow (Uri uri) => uri switch
{
 { Scheme: "http", Port: 80, Host: var host } => host.Length < 1000,
 { Scheme: "https", Port: 443 } => true,
 { Scheme: "ftp", Port: 21 } => true,
 { IsLoopback: true } => true,
 _ => false
};
```

### List Patterns
List patterns (from C# 11) work with any collection type that is countable (with a Count or Length property) and indexable (with an indexer of type int or System.Index).
A list pattern matches a series of elements in square brackets:
```csharp
int[] numbers = { 0, 1, 2, 3, 4 };
Console.Write (numbers is [0, 1, 2, 3, 4]); // True
```
An underscore matches a single element of any value:
```csharp
Console.Write (numbers is [0, 1, _, _, 4]); // True
```
The var pattern also works in matching a single element:
```csharp
Console.Write (numbers is [0, 1, var x, 3, 4] && x > 1); // True
```
Two dots indicate a slice. A slice matches zero or more elements:
```csharp
Console.Write (numbers is [0, .., 4]); // True
```
With arrays and other types that support indices and ranges (see “Indices and Ranges” on page 63), you can follow a slice with a var pattern:
```csharp
Console.Write (numbers is [0, .. var mid, 4] && mid.Contains (2)); // True
```
A list pattern can include at most one slice.
