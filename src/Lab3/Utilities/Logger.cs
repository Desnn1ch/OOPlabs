namespace Itmo.ObjectOrientedProgramming.Lab3.Utilities;

public class Logger : ILogger
{
    private static readonly Lazy<Logger> _instance;

    static Logger()
    {
        _instance = new Lazy<Logger>(() => new Logger(), LazyThreadSafetyMode.ExecutionAndPublication);
    }

    private Logger() { }

    public static Logger Instance => _instance.Value;

    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {DateTime.Now}: {message}");
    }
}
