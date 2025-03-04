using Application.Abstraction;
using Application.Models.ResultType;
using Application.Services;
using Spectre.Console;

namespace Presentation.Controllers.Scenarios.Operations.Withdraw;

public class WithdrawScenario(IUserService service, ICurrentUserManager currentUser) : IScenario
{
    public string Name => "Withdraw";

    public void Run()
    {
        decimal amount = AnsiConsole.Ask<decimal>("Enter the amount to withdraw");
        if (currentUser.User == null)
        {
            return;
        }

        ResultTypeOperation result = service.Withdraw(currentUser.User, amount);

        switch (result)
        {
            case ResultTypeOperation.Success:
                AnsiConsole.MarkupLine("Withdraw successful");
                break;
            case ResultTypeOperation.Failure failure:
                AnsiConsole.MarkupLine(failure.Message);
                break;
        }

        AnsiConsole.Prompt<string>(new TextPrompt<string>(string.Empty).AllowEmpty());
    }
}