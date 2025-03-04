using Application.Models.Models;
using Application.Services;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Controllers.Scenarios.ModeScenaries.UserScenaries;

public class UserScenarioProvider(ICurrentUserManager currentUser) : IScenarioProvider
{
    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (currentUser.Mode is not AccesMode.Unregistered)
        {
            scenario = null;
            return false;
        }

        scenario = new UserScenario(currentUser);
        return true;
    }
}