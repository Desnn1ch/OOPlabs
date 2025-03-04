namespace Application.Models.Models;

public class User
{
    public long Id { get; private set; }

    public string Name { get; private set; }

    public decimal Balance { get; private set; }

    public User(long id, string accountName,  decimal balance = 0)
    {
        Id = id;
        Name = accountName;
        Balance = balance;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        Balance -= amount;
    }
}
