namespace Itmo.ObjectOrientedProgramming.Lab3.ExternalServices;

public class Display
{
    public string CurrentMessage { get; private set; }

    private readonly DisplayDriver _driver;

    public Display(DisplayDriver displayDriver)
    {
        _driver = displayDriver;
        CurrentMessage = string.Empty;
    }

    public void DisplayMessage(string message)
    {
        _driver.Clear();
        _driver.WriteText(message);
        CurrentMessage = message;
    }
}
