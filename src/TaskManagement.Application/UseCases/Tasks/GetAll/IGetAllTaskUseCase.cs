using TaskManagement.Communication.Responses;

namespace TaskManagement.Application.UseCases.Tasks.GetAll;
public interface IGetAllTaskUseCase
{
    Task<ResponseTasksJson> Execute();
}
