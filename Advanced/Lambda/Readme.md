## Lambda Expressions
A lambda expression is an unnamed method written in place of a delegate instance.
The compiler immediately converts the lambda expression to either of the follow‐
ing:
- A delegate instance.
- An expression tree, of type Expression<TDelegate>, representing the code
inside the lambda expression in a traversable object model. This allows the
lambda expression to be interpreted later at runtime.

In the following example, `x => x * x` is a lambda expression:
```csharp
Transformer sqr = x => x * x;
Console.WriteLine (sqr(3)); // 9
delegate int Transformer (int i);
```

### ` (parameters) => expression-or-statement-block `

- A lambda expression’s code can be a statement block instead of an expression. We
  can rewrite our example as follows:
    `x => { return x * x; }; `

- Lambda expressions are used most commonly with the Func and Action delegates,
so you will most often see our earlier expression written as follows: `Func<int,int> sqr = x => x * x;`
- Default Lambda Parameters (C# 12) =>  `void Print (string message = "") => Console.WriteLine (message);`

- Capturing Outer Variables => 
```csharp
int factor = 2;
Func<int, int> multiplier = n => n * factor;
Console.WriteLine (multiplier (3)); // 6
```

### Static Lambdas
```csharp
void Foo()
{
 int factor = 123;
 static int Multiply (int x) => x * 2; // Local static method
}
```

### Lambda Expressions Versus Local Methods
Local methods have the following three advantages:
- They can be recursive (they can call themselves) without ugly hacks.
- They avoid the clutter of specifying a delegate type.
- They incur slightly less overhead.
