using Application.Models.Models;
using Application.Services;

namespace Presentation.Controllers.Scenarios.ModeScenaries.BackToMode;

public class BackScenario(ICurrentUserManager currentUser) : IScenario
{
    public string Name => "Back";

    public void Run()
    {
        currentUser.Mode = AccesMode.Unregistered;
    }
}
