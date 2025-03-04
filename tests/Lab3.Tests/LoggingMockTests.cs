using Itmo.ObjectOrientedProgramming.Lab3;
using Itmo.ObjectOrientedProgramming.Lab3.Entities;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Decorators;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients.Decorators;
using Lab3.Tests.TestMocks;
using Xunit;

namespace Lab3.Tests;

public class LoggingMockTests
{
    [Fact]
    public void ReceiveMessage_WhenMessageIsReceived_ShouldLog()
    {
        var user = new User("Test UserScenaries");
        var mockLogger = new MockLogger();
        var userRecipient = new Recipient(new LoggingEntityDecorator(user, mockLogger));

        Message message = Message.Builder
            .WithTitle("Test Message")
            .WithBody("This is a test")
            .WithImportanceLevel(0)
            .Build();

        userRecipient.SendMessage(message);

        Assert.Equal(1, mockLogger.LogCallCount);
    }

    [Fact]
    public void ReceiveMessage_WhenUserReceivesImportantMessage_ShouldLogOnce()
    {
        var user = new User("Test UserScenaries");
        var mockLogger = new MockLogger();
        var userRecipient = new FilterRecipient(new Recipient(new LoggingEntityDecorator(user, mockLogger)), 10);
        Message message = Message.Builder
            .WithTitle("Test Message")
            .WithBody("This is a test")
            .WithImportanceLevel(0)
            .Build();

        userRecipient.SendMessage(message);
        Assert.Equal(0, mockLogger.LogCallCount);
    }

    [Fact]
    public void UserReceiveMessage_ShouldLog_True()
    {
        var user = new User("Test UserScenaries");
        var mockLogger = new MockLogger();
        var userRecipient = new FilterRecipient(new Recipient(new LoggingEntityDecorator(user, mockLogger)), 2);
        Message.MessageBuilder messageBuilder = new Message.MessageBuilder().WithTitle("Test Message")
            .WithBody("This is a test")
            .WithImportanceLevel(0);
        Message message1 = messageBuilder.Build();
        Message message2 = message1.Copy();
        Message message3 = messageBuilder.WithImportanceLevel(3).Build();
        userRecipient.SendMessage(message1);
        userRecipient.SendMessage(message2);
        userRecipient.SendMessage(message3);
        Assert.Equal(1, mockLogger.LogCallCount);
    }
}
