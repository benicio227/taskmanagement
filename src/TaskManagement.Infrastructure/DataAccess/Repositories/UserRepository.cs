using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories.Users;
using TaskManagement.Exception;
using TaskManagement.Exception.ExceptionsBase;

namespace TaskManagement.Infrastructure.DataAccess.Repositories;
internal class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    private readonly TaskManagementDbContext _dbContext;

    public UserRepository(TaskManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async System.Threading.Tasks.Task Add(User user)
    {
       await _dbContext.Users.AddAsync(user);
    }

    public async System.Threading.Tasks.Task Delete(long id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);

        if(user == null)
        {
            throw new NotFoundException(ResourceErrorMessages.USER_WITH_ID_NOT_WAS_FOUND);
        }


        _dbContext.Remove(user);
    }

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email));
    }

    public async Task<User> GetById(long id)
    {
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id);

        if (user == null)
        {
            throw new NotFoundException(ResourceErrorMessages.USER_WITH_ID_NOT_WAS_FOUND);
        }

        return user;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
    }
}
