using Application.Models.Models;
using Application.Services;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Controllers.Scenarios.ModeScenaries.BackToMode;

public class BackScenarioProvider(ICurrentUserManager currentUser) : IScenarioProvider
{
    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (currentUser.User is not null || currentUser.Mode is AccesMode.Unregistered)
        {
            scenario = null;
            return false;
        }

        scenario = new BackScenario(currentUser);
        return true;
    }
}