using Application.Models.Models;

namespace Application.Abstraction;

public interface IUserRepository
{
    User? FindAccountByUsername(string username);

    User? CreateUser(string username, string pin);

    void UpdateBalance(User user, TransactionType type);

    bool PasswordVerification(long userId, string inputPassword);

    IEnumerable<string> GetTransactions(User user);
}
