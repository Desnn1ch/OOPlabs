using Itmo.ObjectOrientedProgramming.Lab3.ExternalServices;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Adapters;

public class MessengerAdapter : BaseAdapter
{
    private readonly Messenger _messenger;

    public MessengerAdapter(Messenger messenger)
    {
        _messenger = messenger;
    }

    public override void ReceiveMessage(Message message)
    {
        string formattedMessage = FormatMessage(message);
        _messenger.SendMessage(formattedMessage);
    }
}
