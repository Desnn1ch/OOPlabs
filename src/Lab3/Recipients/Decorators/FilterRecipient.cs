using Itmo.ObjectOrientedProgramming.Lab3.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients.Decorators;

public class FilterRecipient : IRecipient
{
    private readonly IRecipient _recipient;
    private readonly int _minImportanceLevel;

    public FilterRecipient(IEntity entity, int minImportanceLevel)
    {
        _recipient = new Recipient(entity);
        _minImportanceLevel = minImportanceLevel;
    }

    public FilterRecipient(IRecipient recipient, int minImportanceLevel)
    {
        _recipient = recipient;
        _minImportanceLevel = minImportanceLevel;
    }

    public void SendMessage(Message message)
    {
        if (message.ImportanceLevel >= _minImportanceLevel)
        {
            _recipient.SendMessage(message);
        }
    }
}
