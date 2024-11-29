using TaskManagement.Communication.Responses;

namespace TaskManagement.Application.UseCases.Users.GetById;
public interface IGetByIdUserUseCase
{
    Task<ResponseUserJson> Execute(long id);
}
