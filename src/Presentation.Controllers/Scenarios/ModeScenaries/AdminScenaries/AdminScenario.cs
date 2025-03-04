using Application.Abstraction;
using Application.Models.Models;
using Application.Services;
using Spectre.Console;

namespace Presentation.Controllers.Scenarios.ModeScenaries.AdminScenaries;

public class AdminScenario(IAdminService service, ICurrentUserManager currentUser) : IScenario
{
    public string Name => "Admin mode";

    public void Run()
    {
        string password = AnsiConsole.Ask<string>("Enter the admin password: ");

        bool result = service.Login(password);

        if (result)
        {
            AnsiConsole.MarkupLine("Successful login");
            currentUser.Mode = AccesMode.Admin;
        }

        AnsiConsole.Prompt<string>(new TextPrompt<string>(string.Empty).AllowEmpty());
    }
}