using Itmo.ObjectOrientedProgramming.Lab3;
using Itmo.ObjectOrientedProgramming.Lab3.Entities;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;
using Itmo.ObjectOrientedProgramming.Lab3.Topics;
using Lab3.Tests.TestMocks;
using Xunit;

namespace Lab3.Tests;

public class AllRecipientsTests
{
    [Fact]
    public void UsersTopic_GroupRecipient_ShouldRecive()
    {
        var user1 = new User("UserScenaries 1");
        IRecipient userRecipient1 = new Recipient(user1);

        var user2 = new User("UserScenaries 2");
        IRecipient userRecipient2 = new Recipient(user2);

        var user3 = new User("UserScenaries 3");
        IRecipient userRecipient3 = new Recipient(user3);

        var groupRecipient = new GroupRecipient();
        groupRecipient.AddRecipient(userRecipient1);
        groupRecipient.AddRecipient(userRecipient2);
        groupRecipient.AddRecipient(userRecipient3);

        Message message = Message.Builder
            .WithTitle("Title")
            .WithBody("Text")
            .Build();

        groupRecipient.SendMessage(message);

        Assert.Equal(1, user1.GetUnreadMessagesCount());
        Assert.Equal(1, user2.GetUnreadMessagesCount());
        Assert.Equal(1, user3.GetUnreadMessagesCount());
    }

    [Fact]
    public void UsersTopic_GroupRecursiveRecipient_ShouldRecive()
    {
        var user1 = new User("UserScenaries 1");
        IRecipient userRecipient1 = new Recipient(user1);

        var groupRecipient1 = new GroupRecipient();
        groupRecipient1.AddRecipient(userRecipient1);

        var user2 = new User("UserScenaries 2");
        IRecipient userRecipient2 = new Recipient(user2);

        var groupRecipient2 = new GroupRecipient();
        groupRecipient2.AddRecipient(userRecipient1);
        groupRecipient2.AddRecipient(userRecipient2);

        var groupRecipient3 = new GroupRecipient();
        var user3 = new User("UserScenaries 3");
        IRecipient userRecipient3 = new Recipient(user3);

        groupRecipient3.AddRecipient(groupRecipient2);
        groupRecipient3.AddRecipient(userRecipient3);

        Message message1 = Message.Builder
            .WithTitle("Title")
            .WithBody("Text")
            .Build();
        Message message2 = message1.Copy();
        Message message3 = message1.Copy();

        groupRecipient1.SendMessage(message1);
        groupRecipient2.SendMessage(message2);
        groupRecipient3.SendMessage(message3);

        Assert.Equal(3, user1.GetUnreadMessagesCount());
        Assert.Equal(2, user2.GetUnreadMessagesCount());
        Assert.Equal(1, user3.GetUnreadMessagesCount());
    }

    [Fact]
    public void Topic_MixRecipients_ShouldRecive()
    {
        var user = new User("UserScenaries 1");
        var userRecipient = new Recipient(user);

        var messenger = new MockExternalEntity();
        var messengerAdapter = new MockAdapter(messenger);
        var messengerAdapterRecipient = new Recipient(messengerAdapter);

        var topic = new Topic("Topic");

        topic.AddRecipient(userRecipient);
        topic.AddRecipient(messengerAdapterRecipient);

        Message message = Message.Builder
            .WithTitle("Title")
            .WithBody("Text")
            .Build();

        topic.SendMessage(message);

        Assert.True(user.TryMarkMessageAsRead(message.Id));
        Assert.Equal("Title: Title\nBody: Text", messenger.LastSentMessage);
    }
}
