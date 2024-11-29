using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;

namespace TaskManagement.Application.UseCases.Login;
public interface IDoLoginUserUseCase
{
    Task<ResponseLoginUserJson> Execute(RequestLoginJson request);
}
