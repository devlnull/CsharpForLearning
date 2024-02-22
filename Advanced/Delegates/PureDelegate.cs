namespace Delegates;

public class PureDelegate
{
    private delegate int Transformer(int x);

    int Square(int x) => x * x;
    int Cube(int x) => x * x * x;

    public void Execute()
    {
        int[] values = {1, 2, 3};
        Transform(values, Square); // Hook in the Square method
        foreach (int i in values)
            Console.Write(i + " "); // 1 4 9
    }

    void Transform(int[] values, Transformer transformer)
    {
        for (int i = 0; i < values.Length; i++)
            values[i] = transformer(values[i]);
    }
}