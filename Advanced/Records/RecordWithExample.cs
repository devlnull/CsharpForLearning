namespace Records;

public class RecordWithExample
{
    public void Execute()
    {
        var range100 = new Range(1, 100);
        var range100Items = GetRange(range100);
        
        var anotherRange = new Range(1, 100);
        var anotherRangeItems = GetRange(anotherRange);

        Console.WriteLine(range100 == anotherRange);
        Console.WriteLine(range100Items == anotherRangeItems);
    }

    record Range(int Start, int Count);

    record RangeItems(params int[] Items);
    RangeItems GetRange(Range range)
    {
        return new RangeItems(Enumerable.Range(range.Start, range.Count).ToArray());
    }
}
