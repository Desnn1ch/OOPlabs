namespace Itmo.ObjectOrientedProgramming.Lab3.Entities;

public class UserMessageStatus
{
    public Message Message { get; }

    public bool IsRead { get; private set; }

    public UserMessageStatus(Message message)
    {
        Message = message;
        IsRead = false;
    }

    public bool MarkAsRead()
    {
        if (IsRead) return false;
        IsRead = true;
        return true;
    }
}