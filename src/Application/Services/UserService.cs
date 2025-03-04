using Application.Abstraction;
using Application.Models.Models;
using Application.Models.ResultType;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public ResultTypeAuth Register(string username, string pin)
    {
        if (_userRepository.FindAccountByUsername(username) != null)
        {
            return new ResultTypeAuth.IncorrectData();
        }

        User? user = _userRepository.CreateUser(username, pin);

        if (user == null)
        {
            return new ResultTypeAuth.IncorrectData();
        }

        return new ResultTypeAuth.Success(user);
    }

    public ResultTypeAuth Login(string username, string pin)
    {
        User? user = _userRepository.FindAccountByUsername(username);

        if (user == null || _userRepository.PasswordVerification(user.Id, pin) is false)
        {
            return new ResultTypeAuth.IncorrectData();
        }

        return new ResultTypeAuth.Success(user);
    }

    public ResultTypeOperation Deposit(User user, decimal amount)
    {
        if (amount <= 0)
        {
            return new ResultTypeOperation.Failure("Amount must be greater than zero");
        }

        user.Deposit(amount);
        _userRepository.UpdateBalance(user, TransactionType.Deposit);
        return new ResultTypeOperation.Success();
    }

    public ResultTypeOperation Withdraw(User user, decimal amount)
    {
        if (amount <= 0)
        {
            return new ResultTypeOperation.Failure("Amount must be greater than zero");
        }

        if (user.Balance < amount)
        {
            return new ResultTypeOperation.Failure("Insufficient funds");
        }

        user.Withdraw(amount);
        _userRepository.UpdateBalance(user, TransactionType.Withdrawal);
        return new ResultTypeOperation.Success();
    }

    public TransactionHistoryResult GetTransactionsHistory(User user)
    {
        try
        {
            IEnumerable<string> transactions = _userRepository.GetTransactions(user);
            return new TransactionHistoryResult.Success(transactions.ToList());
        }
        catch (Exception exeption)
        {
            Console.WriteLine(exeption);
            return new TransactionHistoryResult.Failure();
        }
    }
}
