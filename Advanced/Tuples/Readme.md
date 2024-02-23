## Tuples
Like anonymous types, tuples provide a simple way to store a set of values. Tuples
were introduced into C# with the main purpose of allowing methods to return
multiple values without resorting to out parameters (something you cannot do with
anonymous types). Since then, however, records have been introduced, offering a
concise typed approach that we will describe in the following section.

- NOTE: Tuples do almost everything that anonymous types do and
  have the potential advantage of being value types, but they
  suffer—as you’ll see soon—from runtime type erasure with
  named elements.

The simplest way to create a tuple literal is to list the desired values in parentheses.
This creates a tuple with unnamed elements, which you refer to as Item1, Item2, and
so on:
```csharp
var bob = ("Bob", 23); // Allow compiler to infer the element types
Console.WriteLine (bob.Item1); // Bob
Console.WriteLine (bob.Item2); // 23
```
Tuples are value types, with mutable (read/write) elements:
```csharp
var joe = bob; // joe is a *copy* of bob
joe.Item1 = "Joe"; // Change joe’s Item1 from Bob to Joe
Console.WriteLine (bob); // (Bob, 23)
Console.WriteLine (joe); // (Joe, 23)
```
Unlike with anonymous types, you can specify a tuple type explicitly. Just list each of
the element types in parentheses:
```csharp
(string,int) bob = ("Bob", 23);
```
This means that you can usefully return a tuple from a method:
```csharp
(string,int) person = GetPerson(); // Could use 'var' instead if we want
Console.WriteLine (person.Item1); // Bob
Console.WriteLine (person.Item2); // 23
(string,int) GetPerson() => ("Bob", 23);
```
Tuples play well with generics, so the following types are all legal:
```csharp
Task<(string,int)>
Dictionary<(string,int),Uri>
IEnumerable<(int id, string name)> // See below for naming elements
```

### Naming Tuple Elements
You can optionally give meaningful names to elements when creating tuple literals:
```csharp
var tuple = (name:"Bob", age:23);
Console.WriteLine (tuple.name); // Bob
Console.WriteLine (tuple.age); // 23
```
You can do the same when specifying tuple types:
```csharp
var person = GetPerson();
Console.WriteLine (person.name); // Bob
Console.WriteLine (person.age); // 23
(string name, int age) GetPerson() => ("Bob", 23)
```

### Type erasure
We stated previously that the C# compiler handles anonymous types by building
custom classes with named properties for each of the elements. With tuples, C#
works differently and uses a preexisting family of generic structs:
```csharp
public struct ValueTuple<T1>
public struct ValueTuple<T1,T2>
public struct ValueTuple<T1,T2,T3>
...
```
Each of the ValueTuple<> structs has fields named Item1, Item2, and so on.
Hence, (string,int) is an alias for ValueTuple<string,int>, and this means that
named tuple elements have no corresponding property names in the underlying
types. Instead, the names exist only in the source code, and in the imagination
of the compiler. At runtime, the names mostly disappear, so if you decompile a
program that refers to named tuple elements, you’ll see just references to Item1,
Item2, and so on.

### Aliasing Tuples (C# 12)
From C# 12, you can leverage the using directive to define aliases for tuples:
```csharp
using Point = (int, int);
Point p = (3, 4);
```
This feature also works with tuples that have named elements:
```csharp
using Point = (int X, int Y); // Legal (but not necessarily *good*!)
Point p = (3, 4);
```
Again, we’ll see shortly how records offer a fully typed solution with the same level
of conciseness:
```csharp
Point p = new (3, 4);
record Point (int X, int Y);
```
### Equality Comparison
As with anonymous types, the Equals method performs structural equality comparison. This means that it compares the underlying data rather than the reference:
```csharp
var t1 = ("one", 1);
var t2 = ("one", 1);
Console.WriteLine (t1.Equals (t2)); // True
```
In addition, ValueTuple<> overloads the == and != operators:
```csharp
Console.WriteLine (t1 == t2); // True (from C# 7.3)
```