namespace Application.Models.ResultType;

public abstract record ResultTypeOperation
{
    private ResultTypeOperation() { }

    public sealed record Success() : ResultTypeOperation;

    public sealed record Failure(string Message) : ResultTypeOperation;
}