using Application.Abstraction;
using Application.Models.ResultType;
using Application.Services;
using Spectre.Console;

namespace Presentation.Controllers.Scenarios.Operations.Deposit;

public class DepositScenario(IUserService service, ICurrentUserManager currentUser) : IScenario
{
    public string Name => "Deposit";

    public void Run()
    {
        decimal amount = AnsiConsole.Ask<decimal>("Enter the amount to deposit");
        if (currentUser.User == null)
        {
            return;
        }

        ResultTypeOperation result = service.Deposit(currentUser.User, amount);

        switch (result)
        {
            case ResultTypeOperation.Success:
                AnsiConsole.MarkupLine("Deposit successful");
                break;
            case ResultTypeOperation.Failure failure:
                AnsiConsole.MarkupLine(failure.Message);
                break;
        }

        AnsiConsole.Prompt<string>(new TextPrompt<string>(string.Empty).AllowEmpty());
    }
}
