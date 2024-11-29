using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories.Tasks;
public interface ITasksReadOnlyRepository
{
    Task<List<TaskManagement.Domain.Entities.Task>> GetAll(User user);
    Task<TaskManagement.Domain.Entities.Task?> GetById(User user, long id);
}
