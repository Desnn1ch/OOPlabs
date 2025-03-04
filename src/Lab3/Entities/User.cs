namespace Itmo.ObjectOrientedProgramming.Lab3.Entities;

public class User : IEntity
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    private readonly List<UserMessageStatus> receivedMessages;

    public User(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        receivedMessages = new List<UserMessageStatus>();
    }

    public void ReceiveMessage(Message message)
    {
        receivedMessages.Add(new UserMessageStatus(message));
    }

    public bool TryMarkMessageAsRead(Guid messageId)
    {
        UserMessageStatus? userMessage = receivedMessages.FirstOrDefault(um => um.Message.Id == messageId);
        return userMessage?.MarkAsRead() ?? false;
    }

    public UserMessageStatus? GetMessage(Guid messageId)
    {
        return receivedMessages.FirstOrDefault(um => um.Message.Id == messageId);
    }

    public int GetUnreadMessagesCount()
    {
        return receivedMessages.Count(um => !um.IsRead);
    }
}
