using Application.Abstraction;
using Application.Models.ResultType;
using Application.Services;
using Spectre.Console;

namespace Presentation.Controllers.Scenarios.AuthScenaries.Register;

public class RegisterScenario(IUserService service, ICurrentUserManager currentUser) : IScenario
{
    public string Name => "Register";

    public void Run()
    {
        string username = AnsiConsole.Ask<string>("Enter your username");
        string pin = AnsiConsole.Prompt(new TextPrompt<string>("Enter your PIN").Secret());

        ResultTypeAuth result = service.Register(username, pin);

        string message = string.Empty;
        switch (result)
        {
            case ResultTypeAuth.Success success:
                currentUser.User = success.User;
                message = $"Account registered successfully for {username}!";
                break;
            case ResultTypeAuth.IncorrectData:
                message = "Check the correctness of the entered data";
                break;
        }

        AnsiConsole.Prompt<string>(new TextPrompt<string>(message).AllowEmpty());
    }
}