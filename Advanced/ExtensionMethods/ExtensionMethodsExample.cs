namespace ExtensionMethods;

public class ExtensionMethodsExample
{
    public void Execute()
    {
        string str = "Amir,Hossein,Ghorbani";
        var separated = str.SeparateWithComma();

        str.ToList().ForEach(Console.WriteLine);

        var lowerCaseWork = "banana";
        Console.WriteLine(lowerCaseWork.Capitalize());
    }
}

public static class StringExtensions
{
    public static string[] SeparateWithComma(this string arg)
    {
        return string.IsNullOrEmpty(arg)
            ? new[] {arg}
            : arg.Split(',', StringSplitOptions.RemoveEmptyEntries);
    }

    public static string Capitalize(this string arg)
    {
        return string.IsNullOrEmpty(arg)
            ? arg
            : new string(arg[0].ToString().ToUpper() + arg[1..arg.Length]);
    }
}