using Application.Abstraction;

namespace Application.Services;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _repository;

    public AdminService(IAdminRepository repository)
    {
        _repository = repository;
    }

    public bool Login(string password)
    {
        return _repository.CompareAdminPassword(password);
    }
}