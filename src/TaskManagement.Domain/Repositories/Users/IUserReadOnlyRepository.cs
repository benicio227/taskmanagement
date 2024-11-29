using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories.Users;
public interface IUserReadOnlyRepository
{
    Task<User> GetById(long id);

    Task<bool> ExistActiveUserWithEmail(string email);

    Task<User?> GetUserByEmail(string email);
}
