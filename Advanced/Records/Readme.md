## Records

A record is a special kind of class or struct that’s designed to work well with immutable (read-only) data. Its most
useful feature is nondestructive mutation; however,
records are also useful in creating types that just combine or hold data. In simple
cases, they eliminate boilerplate code while honoring the equality semantics most
suitable for immutable types.
Records are purely a C# compile-time construct. At runtime, the CLR sees them
just as classes or structs (with a bunch of extra “synthesized” members added by the
compiler).

### Background

Writing immutable types (whose fields cannot be modified after initialization) is a
popular strategy for simplifying software and reducing bugs. It’s also a core aspect of
functional programming, where mutable state is avoided and functions are treated
as data. LINQ is inspired by this principle.
In order to “modify” an immutable object, you must create a new one and copy over
the data while incorporating your modifications (this is called nondestructive mutation). In terms of performance, this
is not as inefficient as you might expect, because
a shallow copy will always suffice (a deep copy, where you also copy subobjects and
collections, is unnecessary when data is immutable). But in terms of coding effort,
implementing nondestructive mutation can be very inefficient, especially when
there are many properties. Records solve this problem via a language-supported
pattern.
A second issue is that programmers—particularly functional programmers—some‐
times use immutable types just to combine data (without adding behavior). Defining such types is more work than it
should be, requiring a constructor to assign
each parameter to each public property (a deconstructor may also be useful). With
records, the compiler can do this work for you.
Finally, one of the consequences of an object being immutable is that its identity
cannot change, which means that it’s more useful for such types to implement structural equality than referential
equality. Structural equality means that two instances
are the same if their data is the same (as with tuples). Records give you structural
equality by default—regardless of whether the underlying type is a class or struct—
without any boilerplate code.

### Defining a Record

A record definition is like a class or struct definition, and can contain the same
kinds of members, including fields, properties, methods, and so on. Records can
implement interfaces, and (class-based) records can subclass other (class-based)
records.
By default, the underlying type of a record is a class:

```csharp
record Point { } // Point is a class
```

From C# 10, the underlying type of a record can also be a struct:

```csharp
record struct Point { } // Point is a struct
```

(`record class` is also legal and has the same meaning as `record`)
A simple record might contain just a bunch of init-only properties, and perhaps a
constructor:

```csharp
record Point
{
public Point (double x, double y) => (X, Y) = (x, y);
public double X { get; init; }
public double Y { get; init; }
}
```

Upon compilation, C# transforms the record definition into a class (or struct) and
performs the following additional steps:

- It writes a protected copy constructor (and a hidden Clone method) to facilitate nondestructive mutation.
- It overrides/overloads the equality-related functions to implement structural equality.
- It overrides the ToString() method (to expand the record’s public properties, as with anonymous types).

The preceding record declaration expands into something like this:

```csharp
class Point
{
public Point (double x, double y) => (X, Y) = (x, y);
public double X { get; init; }
public double Y { get; init; }
protected Point (Point original) // “Copy constructor”
{
this.X = original.X; this.Y = original.Y
}
// This method has a strange compiler-generated name:
public virtual Point <Clone>$() => new Point (this); // Clone method
// Additional code to override Equals, ==, !=, GetHashCode, ToString()
// ...
}
```

### Parameter lists

A record definition can be shortened through the use of a parameter list:

```csharp
record Point (double X, double Y)
{
// You can optionally define additional class members here...
}
```

Parameters can include the in and params modifiers, but not out or ref. If a
parameter list is specified, the compiler performs the following extra steps:

- It writes an init-only property per parameter.
- It writes a primary constructor to populate the properties.
- It writes a deconstructor.

This means that if we declare our Point record simply as:

```csharp
record Point (double X, double Y);
```

the compiler will end up generating (almost) exactly what we listed in the preceding
expansion. A minor difference is that the parameter names in the primary construc‐
tor will end up as X and Y instead of x and y:

```csharp
public Point (double X, double Y) // “Primary constructor”
{
    this.X = X; this.Y = Y;
}
```

### Nondestructive Mutation

The most important step that the compiler performs with all records is to write
a copy constructor (and a hidden Clone method). This enables nondestructive mutation via the with keyword:

```csharp
Point p1 = new Point (3, 3);
Point p2 = p1 with { Y = 4 };
Console.WriteLine (p2); // Point { X = 3, Y = 4 }
```

In this example, p2 is a copy of p1, but with its Y property set to 4. The benefit is
more apparent when there are more properties:

```csharp
Test t1 = new Test (1, 2, 3, 4, 5, 6, 7, 8);
Test t2 = t1 with { A = 10, C = 30 };
Console.WriteLine (t2);
```

Here’s the output:

```
Test { A = 10, B = 2, C = 30, D = 4, E = 5, F = 6, G = 7, H = 8 }
```

#### Nondestructive mutation occurs in two phases:

- First, the copy constructor clones the record. By default, it copies each of
  the record’s underlying fields, creating a faithful replica while bypassing (the
  overhead of) any logic in the init accessors. All fields are included (public and
  private, as well as the hidden fields that back automatic properties).
- Then, each property in the member initializer list is updated (this time using
  the init accessors).

### Records and Equality Comparison
Just as with structs, anonymous types, and tuples, records provide structural equal‐
ity out of the box, meaning that two records are equal if their fields (and automatic properties) are equal:
```csharp

var p1 = new Point (1, 2);
var p2 = new Point (1, 2);
Console.WriteLine (p1.Equals (p2)); // True
record Point (double X, double Y);
```
The equality operator also works with records (as it does with tuples):
```csharp
Console.WriteLine (p1 == p2); // True
```
