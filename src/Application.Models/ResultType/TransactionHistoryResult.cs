namespace Application.Models.ResultType;

public abstract record TransactionHistoryResult
{
    private TransactionHistoryResult() { }

    public sealed record Success(IEnumerable<string> Transactions) : TransactionHistoryResult;

    public sealed record Failure() : TransactionHistoryResult;
}
