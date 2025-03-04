using Microsoft.Extensions.DependencyInjection;
using Presentation.Controllers.Scenarios.AuthScenaries.Login;
using Presentation.Controllers.Scenarios.AuthScenaries.LogOut;
using Presentation.Controllers.Scenarios.AuthScenaries.Register;
using Presentation.Controllers.Scenarios.ModeScenaries.AdminScenaries;
using Presentation.Controllers.Scenarios.ModeScenaries.BackToMode;
using Presentation.Controllers.Scenarios.ModeScenaries.UserScenaries;
using Presentation.Controllers.Scenarios.Operations.CheckTransactions;
using Presentation.Controllers.Scenarios.Operations.Deposit;
using Presentation.Controllers.Scenarios.Operations.ViewBalance;
using Presentation.Controllers.Scenarios.Operations.Withdraw;

namespace Presentation.Controllers.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, AdminScenarioProvider>();
        collection.AddScoped<IScenarioProvider, UserScenarioProvider>();

        collection.AddScoped<IScenarioProvider, LoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, RegisterScenarioProvider>();

        collection.AddScoped<IScenarioProvider, BackScenarioProvider>();

        collection.AddScoped<IScenarioProvider, ViewBalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, DepositScenarioProvider>();
        collection.AddScoped<IScenarioProvider, WithdrawScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CheckTransactionsScenarioProvider>();

        collection.AddScoped<IScenarioProvider, LogOutScenarioProvider>();

        return collection;
    }
}