using Itmo.ObjectOrientedProgramming.Lab3.Utilities;

namespace Lab3.Tests.TestMocks;

public class MockLogger : ILogger
{
    public int LogCallCount { get; private set; }

    public void Log(string message)
    {
        LogCallCount++;
    }
}
