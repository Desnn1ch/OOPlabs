using Application.Models.Models;
using Application.Models.ResultType;

namespace Application.Abstraction;

public interface IUserService
{
    ResultTypeAuth Register(string username, string pin);

    ResultTypeAuth Login(string username, string pin);

    ResultTypeOperation Deposit(User user, decimal amount);

    ResultTypeOperation Withdraw(User user, decimal amount);

    TransactionHistoryResult GetTransactionsHistory(User user);
}