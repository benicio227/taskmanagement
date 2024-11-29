namespace TaskManagement.Domain.Repositories.Tasks;
public interface ITasksWriteOnlyRepository
{
    Task Add(TaskManagement.Domain.Entities.Task task);
    Task Delete(long id);
    Task<int> GetPendingTasksCount(long userId); 

}
