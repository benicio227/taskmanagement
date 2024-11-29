using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;

namespace TaskManagement.Application.UseCases.Tasks.Register;
public interface IRegisterTaskUseCase
{
    Task<ResponseTaskJson> Execute(RequestTaskJson request);
}
