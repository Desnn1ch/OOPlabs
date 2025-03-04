namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Adapters;

public abstract class BaseAdapter : IEntity
{
    public abstract void ReceiveMessage(Message message);

    protected virtual string FormatMessage(Message message)
    {
        return $"Title: {message.Title}\nBody: {message.Body}";
    }
}