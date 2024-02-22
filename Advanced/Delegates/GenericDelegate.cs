namespace Delegates;

public class GenericDelegate
{
    public delegate T Transformer<T>(T arg);

    public void Execute()
    {
        int[] values = {1, 2, 3};
        Util.Transform(values, Square); // Hook in Square
        foreach (int i in values)
            Console.Write(i + " "); // 1 4 9
    }

    int Square(int x) => x * x;

    public class Util
    {
        public static void Transform<T>(T[] values, Transformer<T> transformer)
        {
            for (int i = 0; i < values.Length; i++)
                values[i] = transformer(values[i]);
        }
    }
}