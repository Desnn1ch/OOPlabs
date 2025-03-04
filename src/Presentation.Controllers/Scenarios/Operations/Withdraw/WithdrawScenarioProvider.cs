using Application.Abstraction;
using Application.Services;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Controllers.Scenarios.Operations.Withdraw;

public class WithdrawScenarioProvider(ICurrentUserManager currentUser, IUserService service) : IScenarioProvider
{
    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (currentUser.User is null)
        {
            scenario = null;
            return false;
        }

        scenario = new WithdrawScenario(service, currentUser);
        return true;
    }
}