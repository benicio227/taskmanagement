
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories.Tasks;
public interface ITasksUpdateOnlyRepository
{
   Task<Domain.Entities.Task> GetById(User user, long id);
   void Update(Entities.Task task);
   //void Update(Task<Entities.Task> task);
}
