using AutoMapper;
using TaskManagement.Communication.Responses;
using TaskManagement.Domain.Repositories.Tasks;
using TaskManagement.Domain.Services.LoggedUser;

namespace TaskManagement.Application.UseCases.Tasks.GetAll;
public class GetAllTaskUseCase : IGetAllTaskUseCase
{
    private readonly ITasksReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public GetAllTaskUseCase(
        ITasksReadOnlyRepository repository,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
       _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseTasksJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

       //O método GetAll retorna uma lista de Task  List<Task>
        var tasks = await _repository.GetAll(loggedUser);


        //Aqui estamos mapeando os dados do objeto/entidade Tasks para o objeto ResponseShortTaskJson
        //Veja que como o método GetAll está retornando uma lista de Task, eu preciso de uma lista de ResponseShortTaskJson também
        var tasksDto = _mapper.Map<List<ResponseShortTaskJson>>(tasks); //Retorna a lista ResponseShortTaskJson já preenchida com os dados da entidade Task


        return new ResponseTasksJson
        {
            Tasks = tasksDto //Aqui estamos inserindo a lista List<ResponseShortTaskJson> dentro da propriedade Tasks
        };
    }
}
