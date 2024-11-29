using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories.Users;
public interface IUserWriteOnlyRepository
{
    System.Threading.Tasks.Task Add(User user);

    System.Threading.Tasks.Task Delete(long id);
}
