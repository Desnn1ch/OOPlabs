using Application.Abstraction;
using Application.Models.Models;
using Application.Services;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Controllers.Scenarios.AuthScenaries.Register;

public class RegisterScenarioProvider(ICurrentUserManager currentUser, IUserService service) : IScenarioProvider
{
    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (currentUser.User is not null || currentUser.Mode is AccesMode.Unregistered)
        {
            scenario = null;
            return false;
        }

        scenario = new RegisterScenario(service, currentUser);
        return true;
    }
}