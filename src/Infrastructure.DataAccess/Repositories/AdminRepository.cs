using Application.Abstraction;
using DotNetEnv;

namespace Infrastructure.DataAccess.Repositories;

public class AdminRepository : IAdminRepository
{
    public bool CompareAdminPassword(string password)
    {
        const string envPath = "../../../../../.env";

        Env.Load(envPath);

        string? adminPassword = Environment.GetEnvironmentVariable("admin_password");

        return adminPassword == password;
    }
}
