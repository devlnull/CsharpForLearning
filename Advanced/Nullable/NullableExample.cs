namespace Nullable;

public class NullableExample
{
    public void Execute()
    {
        int val = 10;
        int? val1 = 10;
        Nullable<int> val2 = 10;
        
        if(val == val1 && val == val2 && val1 == val2)
            Console.WriteLine("All are equal");
    }
}
