using Itmo.ObjectOrientedProgramming.Lab3.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients;

public class Recipient : IRecipient
{
    private readonly IEntity _entity;

    public Recipient(IEntity entity)
    {
        _entity = entity;
    }

    public void SendMessage(Message message)
    {
        ProcessMessage(message);
    }

    private void ProcessMessage(Message message)
    {
        _entity.ReceiveMessage(message);
    }
}
