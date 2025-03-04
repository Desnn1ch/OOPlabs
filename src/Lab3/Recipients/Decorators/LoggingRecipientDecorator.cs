using Itmo.ObjectOrientedProgramming.Lab3.Utilities;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients.Decorators;

public class LoggingRecipientDecorator : IRecipient
{
    private readonly IRecipient _recipient;
    private readonly ILogger _logger;

    public LoggingRecipientDecorator(IRecipient recipient, ILogger logger)
    {
        _recipient = recipient;
        _logger = logger;
    }

    public void SendMessage(Message message)
    {
        _logger.Log($"Attempting to send message: {message.Title}, Importance: {message.ImportanceLevel}");
        _recipient.SendMessage(message);
    }
}
