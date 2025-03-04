using Application.Services;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Controllers.Scenarios.AuthScenaries.LogOut;

public class LogOutScenarioProvider(ICurrentUserManager currentUser) : IScenarioProvider
{
    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (currentUser.User is null)
        {
            scenario = null;
            return false;
        }

        scenario = new LogOutScenario(currentUser);
        return true;
    }
}
