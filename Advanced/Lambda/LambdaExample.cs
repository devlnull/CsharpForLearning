namespace Lambda;

public class LambdaExample
{
    void Print (string message = "") => Console.WriteLine (message);


    public void Execute()
    {
        var square = (int x) => x * x;
        Func<string,string,int> totalLength = (s1, s2) => s1.Length + s2.Length;
        int total = totalLength ("hello", "world"); // total is 10;
        Func<int,int> funcSquare = x => x * x;

    }

}
