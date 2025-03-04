namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients;

public class GroupRecipient : IRecipient
{
    private readonly List<IRecipient> _recipients;

    public GroupRecipient()
    {
        _recipients = new List<IRecipient>();
    }

    public void AddRecipient(IRecipient recipient)
    {
        _recipients.Add(recipient);
    }

    public void SendMessage(Message message)
    {
        ProcessMessage(message);
    }

    protected void ProcessMessage(Message message)
    {
        foreach (IRecipient recipient in _recipients)
        {
            recipient.SendMessage(message);
        }
    }
}
