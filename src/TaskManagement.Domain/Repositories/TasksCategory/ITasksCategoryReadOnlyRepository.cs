using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories.TasksCategory;
public interface ITasksCategoryReadOnlyRepository
{
    Task<List<TaskCategory>> GetAll(long id);

    Task<TaskCategory?> GetById(User user, long id);
}
