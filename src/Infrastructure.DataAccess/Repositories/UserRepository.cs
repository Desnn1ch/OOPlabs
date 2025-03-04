using Application.Abstraction;
using Application.Models.Models;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;
    private readonly decimal _def_balance = 0;

    public UserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public User? FindAccountByUsername(string username)
    {
        const string sql = """
                           SELECT user_id, username, pin, balance
                           FROM users
                           WHERE username = @username;
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("username", username);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        return new User(
            reader.GetInt64(0),
            reader.GetString(1),
            reader.GetDecimal(3));
    }

    public User? CreateUser(string username, string pin)
    {
        const string sql = """
                           INSERT INTO users (username, pin, balance)
                           VALUES (@username, @pin, @balance);
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("username", username)
            .AddParameter("pin", pin)
            .AddParameter("balance", _def_balance);

        command.ExecuteNonQuery();

        return FindAccountByUsername(username);
    }

    public void UpdateBalance(User user, TransactionType type)
    {
        const string sql = """
                           UPDATE users 
                           SET balance = @balance
                           WHERE user_id = @user_id
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default).AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("user_id", user.Id)
            .AddParameter("balance", user.Balance);

        command.ExecuteNonQuery();

        const string transactions = """
                            INSERT INTO transactions (user_id, money, type, date_time)
                            VALUES (@user_id, @money, @type, @date_time);
                            """;
        string transactionType = type switch
        {
            TransactionType.Deposit => "deposit",
            TransactionType.Withdrawal => "withdrawal",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };

        using NpgsqlCommand transactionCommand = new NpgsqlCommand(transactions, connection)
            .AddParameter("user_id", user.Id)
            .AddParameter("money", user.Balance)
            .AddParameter("type", transactionType)
            .AddParameter("date_time", DateTime.Now);

        transactionCommand.ExecuteNonQuery();
    }

    public bool PasswordVerification(long userId, string inputPassword)
    {
        const string sql = """
                           SELECT 1
                           FROM users
                           WHERE user_id = @user_id AND pin = @pin;
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default).AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("user_id", userId)
            .AddParameter("pin", inputPassword);

        using NpgsqlDataReader reader = command.ExecuteReader();

        return reader.Read();
    }

    public IEnumerable<string> GetTransactions(User user)
    {
        const string sql = """
                           SELECT money, type, date_time
                           from transactions
                           where user_id = @user_id
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("user_id", user.Id);

        using NpgsqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            decimal money = reader.GetDecimal(0);
            string transactionType = reader.GetString(1);
            DateTime transactionDate = reader.GetDateTime(2);

            yield return $"{transactionType} {money} at {transactionDate}";
        }
    }
}
