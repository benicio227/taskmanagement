namespace TaskManagement.Domain.Repositories;
public interface IUnitOfWork
{
    Task Commit();
}
