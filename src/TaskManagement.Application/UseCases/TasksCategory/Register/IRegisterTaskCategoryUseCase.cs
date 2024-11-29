using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;

namespace TaskManagement.Application.UseCases.TasksCategory.Register;
public interface IRegisterTaskCategoryUseCase
{
    Task<ResponseTaskCategoryJson> Execute(RequestCategoryJson request);
}
