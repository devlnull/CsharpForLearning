namespace Delegates;

public class StaticMethodDelegate
{
    delegate int Transformer(int x);

    public void Execute()
    {
        Transformer squareTransformer = Test.Square;
        Transformer cubeTransformer = Test.Cube;
        Console.WriteLine(squareTransformer(10));
        Console.WriteLine(cubeTransformer(10));
    }

    private class Test
    {
        public static int Square(int x) => x * x;
        public static int Cube(int x) => x * x * x;
    }
}