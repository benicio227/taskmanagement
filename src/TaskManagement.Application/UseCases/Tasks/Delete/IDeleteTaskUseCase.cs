namespace TaskManagement.Application.UseCases.Tasks.Delete;
public interface IDeleteTaskUseCase
{
    System.Threading.Tasks.Task Execute(long id);
}
