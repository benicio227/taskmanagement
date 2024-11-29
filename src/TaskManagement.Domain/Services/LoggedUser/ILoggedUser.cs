using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    Task<User> Get();
}
