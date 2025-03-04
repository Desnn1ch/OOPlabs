using Itmo.ObjectOrientedProgramming.Lab3.ExternalServices;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Adapters;

public class DisplayAdapter : BaseAdapter
{
    private readonly Display _display;

    public DisplayAdapter(Display display)
    {
        _display = display;
    }

    public override void ReceiveMessage(Message message)
    {
        string formattedMessage = FormatMessage(message);
        _display.DisplayMessage(formattedMessage);
    }
}
