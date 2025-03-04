using Application.Models.Models;

namespace Application.Services;

public interface ICurrentUserManager
{
    User? User { get; set; }

    AccesMode Mode { get; set; }
}