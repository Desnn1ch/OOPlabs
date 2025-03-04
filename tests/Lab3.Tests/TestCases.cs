using Itmo.ObjectOrientedProgramming.Lab3;
using Itmo.ObjectOrientedProgramming.Lab3.Entities;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Decorators;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients.Decorators;
using Lab3.Tests.TestMocks;
using Xunit;

namespace Lab3.Tests;

public class TestCases
{
    [Fact]
    public void ReceiveMessage_WhenMessageIsReceived_ShouldSaveAsUnread()
    {
        var user = new User("User1");
        Message message = Message.Builder
            .WithTitle("Title")
            .WithBody("Text")
            .Build();
        user.ReceiveMessage(message);
        Assert.False(user.GetMessage(message.Id) is { IsRead: true });
    }

    [Fact]
    public void MarkMessageAsRead_WhenUnreadMessageIsMarked_ShouldChangeStatusToRead()
    {
        var user = new User("User1");
        Message message = Message.Builder
            .WithTitle("Title")
            .WithBody("Text")
            .Build();
        user.ReceiveMessage(message);
        user.TryMarkMessageAsRead(message.Id);

        Assert.True(user.GetMessage(message.Id) is { IsRead: true });
    }

    [Fact]
    public void MarkMessageAsRead_WhenMessageIsAlreadyRead_ShouldReturnError()
    {
        var user = new User("User1");
        Message message = Message.Builder
            .WithTitle("Title")
            .WithBody("Text")
            .Build();
        user.ReceiveMessage(message);
        user.TryMarkMessageAsRead(message.Id);

        bool result = user.TryMarkMessageAsRead(message.Id);

        Assert.False(result);
    }

    [Fact]
    public void ReceiveMessage_WhenMessageHasLowImportance_ShouldNotBeReceived()
    {
        Message message = Message.Builder
            .WithTitle("Title")
            .WithBody("Text")
            .WithImportanceLevel(5)
            .Build();
        var user = new User("Test UserScenaries");
        var mockLogger = new MockLogger();
        var userRecipient = new FilterRecipient(new Recipient(new LoggingEntityDecorator(user, mockLogger)), 10);
        userRecipient.SendMessage(message);

        Assert.False(user.TryMarkMessageAsRead(message.Id));
    }

    [Fact]
    public void ReceiveMessage_WhenMessageIsReceived_ShouldLogMessage()
    {
        var user = new User("Test UserScenaries");
        var mockLogger = new MockLogger();
        var userRecipient = new FilterRecipient(new Recipient(new LoggingEntityDecorator(user, mockLogger)), 5);
        Message message = Message.Builder
            .WithTitle("Title")
            .WithBody("Text")
            .WithImportanceLevel(10)
            .Build();

        userRecipient.SendMessage(message);

        Assert.Equal(1, mockLogger.LogCallCount);

        message = Message.Builder
            .WithTitle("Title")
            .WithBody("Text")
            .WithImportanceLevel(1)
            .Build();
        userRecipient.SendMessage(message);

        Assert.Equal(1, mockLogger.LogCallCount);
    }

    [Fact]
    public void SendMessage_WhenMessageIsSentViaAdapter_ShouldSendMessage()
    {
        var mockMessenger = new MockExternalEntity();
        var adapter = new MockAdapter(mockMessenger);
        Message message = Message.Builder
            .WithTitle("Title")
            .WithBody("Text")
            .Build();

        adapter.ReceiveMessage(message);

        Assert.Equal("Title: Title\nBody: Text", mockMessenger.LastSentMessage);
    }

    [Fact]
    public void ReceiveMessage_WhenMultipleRecipientsWithFilter_ShouldBeReceivedOnce()
    {
        var user = new User("User1");

        var recipient1 = new FilterRecipient(user, minImportanceLevel: 1);
        var recipient2 = new FilterRecipient(user, minImportanceLevel: 3);

        var groupRecipient = new GroupRecipient();
        var filteredRecipient = new FilterRecipient(groupRecipient, minImportanceLevel: 2);
        groupRecipient.AddRecipient(recipient1);
        groupRecipient.AddRecipient(recipient2);

        Message message = Message.Builder
            .WithTitle("Title")
            .WithBody("Text")
            .WithImportanceLevel(2)
            .Build();
        groupRecipient.SendMessage(message);

        Assert.Equal(1, user.GetUnreadMessagesCount());
    }
}
