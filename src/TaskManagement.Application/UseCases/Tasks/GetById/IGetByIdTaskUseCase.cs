using TaskManagement.Communication.Responses;

namespace TaskManagement.Application.UseCases.Tasks.GetById;
public interface IGetByIdTaskUseCase
{
    Task<ResponseTaskJson>Execute(long id);
}
