### Anonymous Types
An anonymous type is a simple class created by the compiler on the fly to store a set
of values. To create an anonymous type, use the new keyword followed by an object
initializer, specifying the properties and values the type will contain; for example:
```csharp
var dude = new { Name = "Bob", Age = 23 };
```
The compiler translates this to (approximately) the following:
```csharp
internal class AnonymousGeneratedTypeName
{
 private string name; // Actual field name is irrelevant
 private int age; // Actual field name is irrelevant
 public AnonymousGeneratedTypeName (string name, int age)
 {
 this.name = name; this.age = age;
 }
 public string Name => name;
 public int Age => age;
 // The Equals and GetHashCode methods are overridden (see Chapter 6).
 // The ToString method is also overridden.
}
...
var dude = new AnonymousGeneratedTypeName ("Bob", 23);
```

You must use the var keyword to reference an anonymous type because it doesn’t
have a name.
The property name of an anonymous type can be inferred from an expression that
is itself an identifier (or ends with one); thus
```csharp
int Age = 23;
var dude = new { Name = "Bob", Age, Age.ToString().Length };
```
is equivalent to the following:
```csharp
var dude = new { Name = "Bob", Age = Age, Length = Age.ToString().Length };
```
Two anonymous type instances declared within the same assembly will have the
same underlying type if their elements are named and typed identically:
```csharp
var a1 = new { X = 2, Y = 4 };
var a2 = new { X = 2, Y = 4 };
Console.WriteLine (a1.GetType() == a2.GetType()); // True
```

Additionally, the Equals method is overridden to perform structural equality comparison (comparison of the data):
```csharp
Console.WriteLine (a1.Equals (a2)); // True
Whereas the equality operator (==) performs referential comparison:
Console.WriteLine (a1 == a2); // False
```
You can create arrays of anonymous types as follows:
```csharp
var dudes = new[]
{
new { Name = "Bob", Age = 30 },
new { Name = "Tom", Age = 40 }
};
```

A method cannot (usefully) return an anonymously typed object, because it is illegal
to write a method whose return type is var:
```csharp
var Foo() => new { Name = "Bob", Age = 30 }; // Not legal!
```
(In the following sections, we will describe records and tuples, which offer alternative approaches for returning multiple values from a method.)
Anonymous types are immutable, so instances cannot be modified after creation.
However, from C# 10, you can use the with keyword to create a copy with variations
(nondestructive mutation):
```csharp
var a1 = new { A = 1, B = 2, C = 3, D = 4, E = 5 };
var a2 = a1 with { E = 10 };
Console.WriteLine (a2); // { A = 1, B = 2, C = 3, D = 4, E = 10 }
```