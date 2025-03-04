namespace Itmo.ObjectOrientedProgramming.Lab3;

public class Message : IClone<Message>
{
    public Guid Id { get; private init; }

    public string Title { get; private set; }

    public string Body { get; private set; }

    public int ImportanceLevel { get; private set; }

    private Message(Guid id, string title, string body, int importanceLevel)
    {
        Id = id;
        Title = title;
        Body = body;
        ImportanceLevel = importanceLevel;
    }

    public Message Copy()
    {
        return new Message(Guid.NewGuid(), Title, Body, ImportanceLevel);
    }

    public static MessageBuilder Builder => new MessageBuilder();

    public class MessageBuilder
    {
        private readonly Guid _id = Guid.NewGuid();
        private string _title = string.Empty;
        private string _body = string.Empty;
        private int _importanceLevel = 0;

        public MessageBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public MessageBuilder WithBody(string body)
        {
            _body = body;
            return this;
        }

        public MessageBuilder WithImportanceLevel(int importanceLevel)
        {
            _importanceLevel = importanceLevel;
            return this;
        }

        public Message Build()
        {
            return new Message(_id, _title, _body, _importanceLevel);
        }
    }
}