using Application.Abstraction;
using Application.Services;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Controllers.Scenarios.Operations.Deposit;

public class DepositScenarioProvider(ICurrentUserManager currentUser, IUserService service) : IScenarioProvider
{
    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (currentUser.User is null)
        {
            scenario = null;
            return false;
        }

        scenario = new DepositScenario(service, currentUser);
        return true;
    }
}
