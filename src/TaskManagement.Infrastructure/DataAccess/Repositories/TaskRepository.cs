using Microsoft.EntityFrameworkCore;
using TaskManagement.Communication.Enums;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories.Tasks;
using TaskManagement.Exception;
using TaskManagement.Exception.ExceptionsBase;

namespace TaskManagement.Infrastructure.DataAccess.Repositories;
internal class TaskRepository : ITasksWriteOnlyRepository, ITasksReadOnlyRepository, ITasksUpdateOnlyRepository

{
    private readonly TaskManagementDbContext _dbContext;
    public TaskRepository(TaskManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async System.Threading.Tasks.Task Add(TaskManagement.Domain.Entities.Task task)
    {

        await _dbContext.Tasks.AddAsync(task);
    }

    public async System.Threading.Tasks.Task Delete(long id)
    {

        var task = await _dbContext.Tasks.FirstAsync(task => task.Id == id);

        _dbContext.Tasks.Remove(task);
 
    }

    public async Task<List<Domain.Entities.Task>> GetAll(User user)
    {
        //AsNoTracking() -> Diz ao EntityFramework para NÃO RASTREAR os objetos retornados por essa consulta
        //Quando as entidades não precisam ser alteradas após serem recuperadas, AsNoTracking REDUZ O USO DE MEMORIA
        //e MELHORA O DESEMPENHO, já que o EF Core não precisa monitorar essas entidades.
        var tasks = await _dbContext.Tasks.AsNoTracking().Where(task => task.UserId == user.Id).ToListAsync();

        if (tasks == null)
        {
            throw new NotFoundException(ResourceErrorMessages.NO_TASKS_WERE_FOUND);
        }

        return tasks;
    }

    async Task<Domain.Entities.Task?> ITasksReadOnlyRepository.GetById(User user, long id)
    {
        //O método FirstOrDefault é um método LINQ que é usado para buscar o primeiro elemento de uma coleção que satisfaça
        //uma determinada condição. Se nenhum elemento for encontrado, ele retorna o valor padrão(null);
        return await _dbContext.Tasks.AsNoTracking().FirstOrDefaultAsync(task => task.Id == id && task.UserId == user.Id);

        //Sempre preciamos verificar se o método FirstOrDefault irá retornar um valor nulo
  
    }

    async Task<Domain.Entities.Task> ITasksUpdateOnlyRepository.GetById(User user, long id)
    {
        var task = await _dbContext.Tasks.FirstOrDefaultAsync(task => task.Id == id && task.UserId == user.Id);

        if (task == null)
        {
            throw new NotFoundException(ResourceErrorMessages.TASK_WITH_ID_WAS_NOT_FOUND);
        }

        return task;
    }

    public void Update(Domain.Entities.Task task)
    {
        _dbContext.Tasks.Update(task);
    }

    public async Task<int> GetPendingTasksCount(long userId)
     // Ele aceita um parâmetro "userIdentifier" do tipo string, que serve para identificar o usuário cujas tarefas serão filtradas
     // return await _dbContext.Tasks => estamos acessando a tabela (Tasks) do banco de dados
     // _dbContext é a instância do contexto de banco de dados que gerencia a conexão com o banco e a manipulação de dados
     // O método "Where" é usado para aplicar um filtro na tabela de tarefas.
     // task.UserIdentifier == userIdentifier => Seleciona apenas as tarefas que pertencem ao usuário cujo identtificador foi passado como parâmetro
     // (int)task.Type == (int)TaskType.Pending => Converte o valor do enum TaskType(task.Type) para um número inteiro
     // Compara esse número com o valor inteiro correspondente ao tipo TaskType.Pending (que é 0)
     // O método CountAsync() conta o número de registros que atendem aos critérios definidos no .Where
     // retorna o total de tarefas pendentes do usuário especificado.
    {
        return await _dbContext.Tasks
            .Where(task => task.UserId == userId && (int)task.Type == (int)TaskType.Pending)
            .CountAsync();
    }

}
