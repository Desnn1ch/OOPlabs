using Application.Abstraction;
using Application.Models.ResultType;
using Application.Services;
using Spectre.Console;

namespace Presentation.Controllers.Scenarios.AuthScenaries.Login;

public class LoginScenario(IUserService service, ICurrentUserManager currentUser) : IScenario
{
    public string Name => "Login";

    public void Run()
    {
        string username = AnsiConsole.Ask<string>("Enter your username");
        string pin = AnsiConsole.Prompt(new TextPrompt<string>("Enter your PIN").Secret());

        ResultTypeAuth result = service.Login(username, pin);

        switch (result)
        {
            case ResultTypeAuth.Success success:
                currentUser.User = success.User;
                AnsiConsole.MarkupLine("Successful login");
                break;
            case ResultTypeAuth.IncorrectData:
                AnsiConsole.MarkupLine("Incorrect username or password");
                break;
        }

        AnsiConsole.Prompt<string>(new TextPrompt<string>(string.Empty).AllowEmpty());
    }
}