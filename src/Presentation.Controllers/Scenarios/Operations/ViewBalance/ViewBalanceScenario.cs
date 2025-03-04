using Application.Services;
using Spectre.Console;

namespace Presentation.Controllers.Scenarios.Operations.ViewBalance;

public class ViewBalanceScenario(ICurrentUserManager currentUser) : IScenario
{
    public string Name => "Balance";

    public void Run()
    {
        if (currentUser.User == null)
        {
            return;
        }

        AnsiConsole.Prompt<string>(new TextPrompt<string>($"Current balance: {currentUser.User.Balance}").AllowEmpty());
    }
}
