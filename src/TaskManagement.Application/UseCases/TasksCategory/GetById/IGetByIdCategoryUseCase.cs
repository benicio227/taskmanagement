using TaskManagement.Communication.Responses;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.UseCases.TasksCategory.GetById;
public interface IGetByIdCategoryUseCase
{
    Task<ResponseTaskCategoryJson> Execute(long id);
}
