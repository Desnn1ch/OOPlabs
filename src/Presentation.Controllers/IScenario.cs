namespace Presentation.Controllers;

public interface IScenario
{
    string Name { get; }

    void Run();
}