namespace TaskManagement.Application.UseCases.Users.Delete;
public interface IDeleteUserUseCase
{
    System.Threading.Tasks.Task Execute(long id);
}
