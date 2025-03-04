using Application.Services;
using Spectre.Console;

namespace Presentation.Controllers.Scenarios.AuthScenaries.LogOut;

public class LogOutScenario(ICurrentUserManager currentUser) : IScenario
{
    public string Name => "Log out";

    public void Run()
    {
        currentUser.User = null;

        AnsiConsole.Prompt<string>(new TextPrompt<string>("Successfully log out").AllowEmpty());
    }
}
