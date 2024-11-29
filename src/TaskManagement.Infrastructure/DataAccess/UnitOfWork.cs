using TaskManagement.Domain.Repositories;

namespace TaskManagement.Infrastructure.DataAccess;
internal class UnitOfWork : IUnitOfWork
{
    private readonly TaskManagementDbContext _dbContext;

    public UnitOfWork(TaskManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}
