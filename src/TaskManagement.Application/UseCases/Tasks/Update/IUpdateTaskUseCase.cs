using TaskManagement.Communication.Requests;

namespace TaskManagement.Application.UseCases.Tasks.Update;
public interface IUpdateTaskUseCase
{
    System.Threading.Tasks.Task Execute(long id, RequestTaskJson request);
}
