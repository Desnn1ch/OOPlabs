using Application.Models.Models;
using Application.Services;

namespace Presentation.Controllers.Scenarios.ModeScenaries.UserScenaries;

public class UserScenario(ICurrentUserManager currentUser) : IScenario
{
    public string Name => "User mode";

    public void Run()
    {
        currentUser.Mode = AccesMode.User;
    }
}