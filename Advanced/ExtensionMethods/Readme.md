## Extension Methods
Extension methods allow an existing type to be extended with new methods without
altering the definition of the original type. An extension method is a static method
of a static class, where the this modifier is applied to the first parameter.
The type of the first parameter will be the type that is extended:

```csharp
public static class StringHelper
{
 public static bool IsCapitalized (this string s)
 {
 if (string.IsNullOrEmpty(s)) return false;
 return char.IsUpper (s[0]);
 }
```

### Extension Method Chaining
Extension methods, like instance methods, provide a tidy way to chain functions.
Consider the following two functions:
```csharp
public static class StringHelper
{
public static string Pluralize (this string s) {...}
public static string Capitalize (this string s) {...}
}
```

x and y are equivalent, and both evaluate to "Sausages", but x uses extension
methods, whereas y uses static methods:

```csharp
string x = "sausage".Pluralize().Capitalize();
string y = StringHelper.Capitalize (StringHelper.Pluralize ("sausage"));
```

### Extension methods versus instance methods
Any compatible instance method will always take precedence over an extension
method. In the following example, Test’s Foo method will always take precedence,
even when called with an argument x of type int:
```csharp 
class Test
{
public void Foo (object x) { } // This method always wins
}
static class Extensions
{
public static void Foo (this Test t, int x) { }
}
```
