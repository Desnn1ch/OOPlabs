using Crayon;
using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.ExternalServices;

public class DisplayDriver
{
    private readonly string _logFilePath;
    private Color _color;

    public DisplayDriver(string logFilePath)
    {
        _logFilePath = logFilePath;
        _color = Color.White;
    }

    public void Clear()
    {
        Console.Clear();
    }

    public void SetColor(Color color)
    {
        _color = color;
    }

    public void WriteText(string text)
    {
        string coloredText = Modify(text);
        Console.WriteLine(coloredText);
        LogToFile(coloredText);
    }

    private string Modify(string value)
    {
        return Output.Rgb(_color.R, _color.G, _color.B).Text(value);
    }

    private void LogToFile(string message)
    {
        using var writer = new StreamWriter(_logFilePath, true);
        writer.WriteLine(message);
    }
}
