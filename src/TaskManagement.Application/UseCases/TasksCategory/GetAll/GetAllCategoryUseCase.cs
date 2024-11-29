using AutoMapper;
using TaskManagement.Communication.Responses;
using TaskManagement.Domain.Repositories.TasksCategory;
using TaskManagement.Domain.Services.LoggedUser;
using TaskManagement.Exception;
using TaskManagement.Exception.ExceptionsBase;

namespace TaskManagement.Application.UseCases.TasksCategory.GetAll;
public class GetAllCategoryUseCase : IGetAllCategoryUseCase
{
    private readonly ITasksCategoryReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public GetAllCategoryUseCase(
        ITasksCategoryReadOnlyRepository repository,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseCategorysJson> Execute()
    {
        var loggedUser =  await _loggedUser.Get();

        var tasksCategory = await _repository.GetAll(loggedUser.Id);

        if (tasksCategory is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TASK_WITH_ID_WAS_NOT_FOUND);
        }

        //Está sendo feito o mapeamento da entidade "TaskCategory" para o objeto "ResponseShortCategoryJson"
        //Name de TaskCategory -> Name de ResponseShortCategoryJson
        //Description de TaskCategory -> Description de ResponseShortCategoryJson
        //Tasks de TaskCategory(do tipo ICollection<Task>) -> Tasks de ResponseShortCategoryJson(do tipo List<ResponseShortTaskJson>)
        //Veja que os nones das propriedades devem ser iguais em ambos os objetos
        var categoryDto = _mapper.Map<List<ResponseShortCategoryJson>>(tasksCategory);
        

        return new ResponseCategorysJson
        {
            Categorys = categoryDto //Aqui pego a lista mapeada de ResponseShortCategoryJson
        };
    }
}
