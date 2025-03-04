namespace Application.Abstraction;

public interface IAdminRepository
{
    bool CompareAdminPassword(string password);
}
