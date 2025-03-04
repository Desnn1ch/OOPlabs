using Itmo.ObjectOrientedProgramming.Lab3.Utilities;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Decorators;

public class LoggingEntityDecorator : IEntity
{
    private readonly IEntity _entity;
    private readonly ILogger _logger;

    public LoggingEntityDecorator(IEntity entity, ILogger logger)
    {
        _entity = entity;
        _logger = logger;
    }

    public void ReceiveMessage(Message message)
    {
        _logger.Log($"[LOG] Recived message: {message.Title}, Importance: {message.ImportanceLevel}");
        _entity.ReceiveMessage(message);
    }
}
