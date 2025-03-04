using Application.Models.Models;

namespace Application.Models.ResultType;

public abstract record ResultTypeAuth
{
    private ResultTypeAuth() { }

    public sealed record Success(User User) : ResultTypeAuth;

    public sealed record IncorrectData() : ResultTypeAuth;
}