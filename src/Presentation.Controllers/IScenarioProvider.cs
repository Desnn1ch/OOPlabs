using System.Diagnostics.CodeAnalysis;

namespace Presentation.Controllers;

public interface IScenarioProvider
{
    bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario);
}