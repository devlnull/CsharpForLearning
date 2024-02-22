namespace Enumeration;

public class EnumerationExample
{
    public void Execute()
    {
        var list = new List<int> {1, 2, 3};
        var dict = new Dictionary<int, string>()
        {
            [3] = "three",
            [10] = "ten"
        };

        IEnumerable<int> enumerable = new[] {1, 2, 3, 4, 5};
        foreach (var item in enumerable)
        {
            Console.WriteLine(item);
        }
    }
}

/*
class Enumerator // Typically implements IEnumerator or IEnumerator<T>
{
    public IteratorVariableType Current { get {...} }
    public bool MoveNext() {...}
}
class Enumerable // Typically implements IEnumerable or IEnumerable<T>
{
    public Enumerator GetEnumerator() {...}
}
*/