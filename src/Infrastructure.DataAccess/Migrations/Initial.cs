using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return """
            CREATE TABLE users
            (
                user_id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                username TEXT NOT NULL unique,
                pin TEXT NOT NULL,
                balance BIGINT NOT NULL
            );
        
            CREATE TABLE transactions
            (
                transaction_id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                user_id BIGINT NOT NULL,
                money BIGINT NOT NULL,
                type TEXT NOT NULL,
                date_time TIMESTAMP NOT NULL,
                FOREIGN KEY (user_id) REFERENCES users(user_id)
            );
        """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider)
    {
        return """
            DROP TABLE users;
            DROP TABLE transactions;
            """;
    }
}
