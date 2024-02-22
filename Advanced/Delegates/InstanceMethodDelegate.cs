namespace Delegates;

public class InstanceMethodDelegate
{
    public delegate void ProgressReporter(int percentComplete);

    public void Execute()
    {
        MyReporter reporter = new MyReporter();
        reporter.Prefix = "%Complete: ";
        
        ProgressReporter progressReporter = reporter.ReportProgress;
        progressReporter(99); // %Complete: 99
        Console.WriteLine(progressReporter.Target == reporter); // True

        Console.WriteLine(progressReporter.Method); // Void ReportProgress(Int32)
        reporter.Prefix = "";
        progressReporter(99); // 99
    }
}

class MyReporter
{
    public string Prefix = "";

    public void ReportProgress(int percentComplete)
        => Console.WriteLine(Prefix + percentComplete);
}