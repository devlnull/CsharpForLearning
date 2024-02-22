namespace Delegates;

public class MulticastDelegate
{
    public delegate int Transformer(int x);

    public int Square(int x) => x * x;
    public int Cube(int x) => x * x * x;

    public void Execute()
    {
        int val = 10;
        
        //Want to square and cube at the same action
        Transformer squareTransformer = Square;
        Transformer cubeTransformer = Cube;

        squareTransformer += cubeTransformer;
        
        //also can remove it like below
        //squareTransformer -= cubeTransformer;

        Console.WriteLine(squareTransformer(val)); // it will square the val then cube it.

    }
}