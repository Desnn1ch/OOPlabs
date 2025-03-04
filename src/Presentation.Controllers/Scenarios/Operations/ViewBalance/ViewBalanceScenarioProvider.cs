using Application.Services;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Controllers.Scenarios.Operations.ViewBalance;

public class ViewBalanceScenarioProvider(ICurrentUserManager currentUser) : IScenarioProvider
{
    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (currentUser.User is null)
        {
            scenario = null;
            return false;
        }

        scenario = new ViewBalanceScenario(currentUser);
        return true;
    }
}
