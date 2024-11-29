using AutoMapper;
using TaskManagement.Communication.Responses;
using TaskManagement.Domain.Repositories.Tasks;
using TaskManagement.Domain.Services.LoggedUser;
using TaskManagement.Exception;
using TaskManagement.Exception.ExceptionsBase;

namespace TaskManagement.Application.UseCases.Tasks.GetById;
public class GetByIdTaskUseCase : IGetByIdTaskUseCase
{
    private readonly ITasksReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public GetByIdTaskUseCase(
        ITasksReadOnlyRepository repository,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseTaskJson> Execute(long id)
    {
        var loggedUser =  await _loggedUser.Get();

        //Caso o método FirtsOrDefault encontre a tarefa, ele retorna o objeto Task
        var task = await _repository.GetById(loggedUser, id);


        if (task is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TASK_WITH_ID_WAS_NOT_FOUND);
        }

        //Aqui estou mapeando os dados do objeto/entidade "Task" para o objeto "ResponseTaskJson"
        return _mapper.Map<ResponseTaskJson>(task);
    }
}
