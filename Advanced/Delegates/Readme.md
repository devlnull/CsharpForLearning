## Delegates
A delegate is an object that knows how to call a method.

- A delegate type defines the kind of method that delegate instances can call. Specifically,
it defines the method’s return type and its parameter types. The following
defines a delegate type called Transformer: \
    ``` delegate int Transformer (int x); ``` \
    \
    ``` int Square (int x) { return x * x; } ```

### Instance and Static Method Targets
A delegate’s target method can be a local, static, or instance method. The following
illustrates a static target method:

```
Transformer t = Test.Square;
Console.WriteLine (t(10)); // 100
class Test { public static int Square (int x) => x * x; }
delegate int Transformer (int x);
```

### Func and Action Delegates
With generic delegates, it becomes possible to write a small set of delegate types that
are so general they can work for methods of any return type and any (reasonable)
number of arguments. These delegates are the Func and Action delegates, defined
in the System namespace (the in and out annotations indicate variance, which we
cover in the context of delegates shortly): 

```
delegate TResult Func <out TResult> ();
delegate TResult Func <in T, out TResult> (T arg);
delegate TResult Func <in T1, in T2, out TResult> (T1 arg1, T2 arg2);
... and so on, up to T16
delegate void Action ();
delegate void Action <in T> (T arg);
delegate void Action <in T1, in T2> (T1 arg1, T2 arg2);
... and so on, up to T16
```

### Delegates Versus Interfaces
A delegate design might be a better choice than an interface design if one or more of
these conditions are true: \
• The interface defines only a single method. \
• Multicast capability is needed. \
• The subscriber needs to implement the interface multiple times. 

### Delegate Compatibility
- Type Compatibility \
  Delegate types are all incompatible with one another, even if their signatures are the
  same:
```
D1 d1 = Method1;
D2 d2 = d1; // Compile-time error
void Method1() { }
delegate void D1();
delegate void D2();
```
- Parameter compatibility
  (Contravariance)
```
StringAction sa = new StringAction (ActOnObject);
sa ("hello");
void ActOnObject (object o) => Console.WriteLine (o); // hello
delegate void StringAction (string s);
```
- Return type compatibility
```
ObjectRetriever o = new ObjectRetriever (RetrieveString);
object result = o();
Console.WriteLine (result); // hello
string RetrieveString() => "hello";
delegate object ObjectRetriever();
```