namespace TaskManagement.Application.UseCases.TasksCategory.Delete;
public interface IDeleteByIdTaskCategoryUseCase
{
    System.Threading.Tasks.Task Execute(long id);
}
