namespace Itmo.ObjectOrientedProgramming.Lab3.ExternalServices;

public class Messenger
{
    public Guid Id { get; private set; }

    public string TitleChat { get; private set; }

    public Messenger(string titleChat)
    {
        Id = Guid.NewGuid();
        TitleChat = titleChat;
    }

    public void SendMessage(string message)
    {
        Console.WriteLine($"[Messenger] {message}");
    }
}
