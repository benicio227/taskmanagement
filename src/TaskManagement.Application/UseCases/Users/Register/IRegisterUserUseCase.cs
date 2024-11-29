using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;

namespace TaskManagement.Application.UseCases.Users.Register;
public interface IRegisterUserUseCase
{
    Task<ResponseUserJson> Execute(RequestUserJson request);
}
