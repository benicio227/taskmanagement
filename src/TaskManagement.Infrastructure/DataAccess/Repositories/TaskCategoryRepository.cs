using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories.TasksCategory;
using TaskManagement.Exception;
using TaskManagement.Exception.ExceptionsBase;

namespace TaskManagement.Infrastructure.DataAccess.Repositories;
internal class TaskCategoryRepository : ITasksCategoryWriteOnlyRepository, ITasksCategoryReadOnlyRepository
{
    private readonly TaskManagementDbContext _dbContext;

    public TaskCategoryRepository(TaskManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(TaskCategory taskCategory)
    {
        _dbContext.TaskCategories.Add(taskCategory);
    }

    public async System.Threading.Tasks.Task Delete(User user, long id)
    {
        var taskCategory = await _dbContext.TaskCategories.FirstOrDefaultAsync(TaskCategory => TaskCategory.UserId == user.Id && TaskCategory.Id == id);

        if (taskCategory is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TASK_CATEGORY_WITH_ID_WAS_NOT_FOUND);
        }

        _dbContext.Remove(taskCategory);
    }

    public async Task<List<TaskCategory>> GetAll(long id)
    //Como o método é assíncrono, ele retorna um objeto Task que contém o resultado final do tipo List<TaskCategory>
    //Essa abordagem permite que o método seja executado de forma não bloqueante.
    {
        return await _dbContext.TaskCategories
            .Where(taskCategory => taskCategory.UserId == id)
            .Include(taskCategory => taskCategory.Tasks)
            .ToListAsync();

        //O método include instrui o Entity Framework a carregar os dados relacionados á entidade principal
        //taskCategory representa um item da entidade TaskCategory
        //taskCategory.Tasks -> acessa a propriedade de navegação que aponta para as tarefas relacionadas (do relacionamento um para muitos entre TaskCategory e Task)

    }

    public async Task<TaskCategory?> GetById(User user, long id)
    {
        return await _dbContext.TaskCategories.FirstOrDefaultAsync(taskCategory => taskCategory.UserId == user.Id && taskCategory.Id == id);
    }
}
