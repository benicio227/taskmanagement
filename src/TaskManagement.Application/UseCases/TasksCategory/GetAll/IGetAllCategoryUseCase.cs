using TaskManagement.Communication.Responses;

namespace TaskManagement.Application.UseCases.TasksCategory.GetAll;
public interface IGetAllCategoryUseCase
{
    Task<ResponseCategorysJson> Execute();
}
