using Itmo.ObjectOrientedProgramming.Lab3;
using Itmo.ObjectOrientedProgramming.Lab3.Entities;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;
using Xunit;

namespace Lab3.Tests;

public class ReadMessageTests
{
    [Fact]
    public void MarkMessageAsRead_ExistingMessage_Success()
    {
        var user = new User("UserScenaries");
        var userRecipient = new Recipient(user);
        Message message = Message.Builder
            .WithTitle("Title")
            .WithBody("Text")
            .Build();
        Guid messageId = message.Id;
        userRecipient.SendMessage(message);
        Assert.True(user.TryMarkMessageAsRead(messageId));
    }

    [Fact]
    public void MarkMessageAsRead_ExistingMessage_Failure()
    {
        var user = new User("UserScenaries");
        var userRecipient = new Recipient(user);
        Message message = Message.Builder
            .WithTitle("Title")
            .WithBody("Text")
            .Build();
        Guid messageId = message.Id;
        userRecipient.SendMessage(message);
        user.TryMarkMessageAsRead(messageId);
        Assert.False(user.TryMarkMessageAsRead(messageId));
    }
}
