using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories.TasksCategory;
public interface ITasksCategoryWriteOnlyRepository
{
    void Add(TaskCategory taskCategory);

    System.Threading.Tasks.Task Delete(User user, long id);
}
