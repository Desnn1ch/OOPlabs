namespace Lab3.Tests.TestMocks;

public class MockExternalEntity
{
    public string LastSentMessage { get; private set; }

    public MockExternalEntity()
    {
        LastSentMessage = string.Empty;
    }

    public void SendMessage(string message)
    {
        LastSentMessage = message;
    }
}
