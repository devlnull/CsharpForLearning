## Anonymous Methods

Anonymous methods are a C# 2.0 feature that was mostly subsumed by C# 3.0’s
lambda expressions. An anonymous method is like a lambda expression, but it lacks
the following features:
- Implicitly typed parameters
- Expression syntax (an anonymous method must always be a statement block)
- The ability to compile to an expression tree, by assigning to Expression<T>

An anonymous method uses the delegate keyword followed (optionally) by a parameter declaration and then a method body. For example:
```csharp
Transformer sqr = delegate (int x) {return x * x;};
Console.WriteLine (sqr(3)); // 9
delegate int Transformer (int i);
```

```csharp
Transformer sqr = (int x) => {return x * x;};
```

