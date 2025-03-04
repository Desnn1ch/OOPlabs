using Itmo.ObjectOrientedProgramming.Lab3;
using Itmo.ObjectOrientedProgramming.Lab3.Entities;

namespace Lab3.Tests.TestMocks;

public class MockAdapter : IEntity
{
    private readonly MockExternalEntity _mockMessenger;

    public MockAdapter(MockExternalEntity mockMessenger)
    {
        _mockMessenger = mockMessenger;
    }

    public void ReceiveMessage(Message message)
    {
        string formattedMessage = $"Title: {message.Title}\nBody: {message.Body}";
        _mockMessenger.SendMessage(formattedMessage);
    }
}
