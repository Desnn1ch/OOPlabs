using Application.Models.Models;

namespace Application.Services;

public class CurrentUserManager : ICurrentUserManager
{
    public User? User { get; set; }

    public AccesMode Mode { get; set; } = AccesMode.Unregistered;
}