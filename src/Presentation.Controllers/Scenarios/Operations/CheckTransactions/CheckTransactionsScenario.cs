using Application.Abstraction;
using Application.Models.ResultType;
using Application.Services;
using Spectre.Console;

namespace Presentation.Controllers.Scenarios.Operations.CheckTransactions;

public class CheckTransactionsScenario(IUserService userService, ICurrentUserManager currentUser) : IScenario
{
    public string Name => "Transactions history";

    public void Run()
    {
        if (currentUser.User == null)
        {
            return;
        }

        TransactionHistoryResult result = userService.GetTransactionsHistory(currentUser.User);

        switch (result)
        {
            case TransactionHistoryResult.Success success:
                AnsiConsole.MarkupLine("Transactions history:");
                foreach (string transaction in success.Transactions)
                {
                    AnsiConsole.MarkupLine($"- {transaction}");
                }

                break;
            case TransactionHistoryResult.Failure failure:
                AnsiConsole.MarkupLine("Unable to get transactions history");
                break;
        }

        AnsiConsole.Prompt<string>(new TextPrompt<string>(string.Empty).AllowEmpty());
    }
}
