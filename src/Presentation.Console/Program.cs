using Application.Extensions;
using Infrastructure.DataAccess.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Controllers;
using Presentation.Controllers.Extensions;
using Spectre.Console;

var collection = new ServiceCollection();

collection
    .AddApplication()
    .AddInfrastructureDataAccess(configuration =>
    {
        configuration.Host = "localhost";
        configuration.Port = 6432;
        configuration.Username = "desnn1ch";
        configuration.Password = "qweasd123";
        configuration.Database = "lab5";
        configuration.SslMode = "Prefer";
    })
    .AddPresentationConsole();

ServiceProvider provider = collection.BuildServiceProvider();
using IServiceScope scope = provider.CreateScope();

scope.UseInfrastructureDataAccess();

ScenarioRunner scenarioRunner = scope.ServiceProvider
    .GetRequiredService<ScenarioRunner>();

string currentDirectory = Environment.CurrentDirectory;
Console.WriteLine("Current directory: " + currentDirectory);

while (true)
{
    scenarioRunner.Run();
    AnsiConsole.Clear();
}