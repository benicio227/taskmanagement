using AutoMapper;
using TaskManagement.Communication.Responses;
using TaskManagement.Domain.Repositories.TasksCategory;
using TaskManagement.Domain.Services.LoggedUser;
using TaskManagement.Exception;
using TaskManagement.Exception.ExceptionsBase;

namespace TaskManagement.Application.UseCases.TasksCategory.GetById;
public class GetByIdCategoryUseCase : IGetByIdCategoryUseCase
{
    private readonly ITasksCategoryReadOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    public GetByIdCategoryUseCase(
        ITasksCategoryReadOnlyRepository repository,
        ILoggedUser loggedUser,
        IMapper mapper)
    {
        _repository = repository;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }
    public async Task<ResponseTaskCategoryJson> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        var taskCategory = await _repository.GetById(loggedUser, id);

        if (taskCategory is null)
        {
           throw new NotFoundException(ResourceErrorMessages.TASK_CATEGORY_WITH_ID_WAS_NOT_FOUND);
        }

        return _mapper.Map<ResponseTaskCategoryJson>(taskCategory);
    }
}

