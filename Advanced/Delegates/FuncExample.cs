namespace Delegates;

public class FuncExample
{
    public static void Transform<T>(T[] values, Func<T, T> transformer)
    {
        for (int i = 0; i < values.Length; i++)
            values[i] = transformer(values[i]);
    }
}