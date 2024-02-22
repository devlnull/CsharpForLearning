namespace Delegates;

public class MulticastExample
{
    public delegate void ProgressReporter(int percentComplete);

    public class Util
    {
        public static void HardWork(ProgressReporter progressReporter)
        {
            for (int i = 0; i < 10; i++)
            {
                progressReporter(i * 10); // Invoke delegate
                System.Threading.Thread.Sleep(100); // Simulate hard work
            }
        }
    }

    public void Execute()
    {
        ProgressReporter p = WriteProgressToConsole;
        p += WriteProgressToFile;
        Util.HardWork(p);

        void WriteProgressToConsole(int percentComplete)
            => Console.WriteLine(percentComplete);

        void WriteProgressToFile(int percentComplete)
            => System.IO.File.WriteAllText("progress.txt",
                percentComplete.ToString());
    }
}