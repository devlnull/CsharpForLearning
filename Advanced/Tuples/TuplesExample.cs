namespace Tuples;

public class TuplesExample
{
    public void Execute()
    {
        var person = GetPerson();
        Console.WriteLine (person.name); // Bob
        Console.WriteLine (person.age); // 23
        (string name, int age) GetPerson() => ("Bob", 23);
    }
}
