using AutoMapper;
using TaskManagement.Application.UseCases.Task.Register;
using TaskManagement.Communication.Requests;
using TaskManagement.Domain.Repositories;
using TaskManagement.Domain.Repositories.Tasks;
using TaskManagement.Domain.Services.LoggedUser;
using TaskManagement.Exception;
using TaskManagement.Exception.ExceptionsBase;

namespace TaskManagement.Application.UseCases.Tasks.Update;
public class UpdateTaskUseCase : IUpdateTaskUseCase
{
    private readonly ITasksUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public UpdateTaskUseCase(
        ITasksUpdateOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async System.Threading.Tasks.Task Execute(long id, RequestTaskJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        //Retorna o objeto/entidade Task rastreada pelo EntityFramework
        //O método FirstOrDefault é um método LINQ que é usado para buscar o primeiro elemento de uma coleção que satisfaça
        //uma determinada condição. Se nenhum elemento for encontrado, ele retorna o valor padrão(null);
        var task = await _repository.GetById(loggedUser, id);
       

        //Verifico primeiro se o id da task que quero atualizar existe
        if (task is null || task.UserId !=  loggedUser.Id)
        {
            throw new NotFoundException(ResourceErrorMessages.TASK_WITH_ID_WAS_NOT_FOUND);
        }

        //Existindo eu mapeio do objeto request "RequestTaskJson" para o objeto/entidade Task
      
        //Se eu fizer dessa forma abaixo, o método Map vai criar uma nova instância de "Task", e vai ser preenchida com
        //O objeto request, logo, ela não será a mesma instância em "var task = await  _repository.GetById(id);"
        //E consequentemente o EntityFramework não vai conseguir rastrear essa entidade. Ela será uma nova entidade,
        //O que levará a ter um comportamento de adicionar essa entidade quando passar para o método Update()
        //task = _mapper.Map<Domain.Entities.Task>(request);

        //Aqui estou mapeando diretamente para o objeto/entidade já existente
        _mapper.Map(request, task);

        //Aqui adiciono o objeto/entidade já rastreada pelo EntityFramework ao método Update
        _repository.Update(task);

 
        await _unitOfWork.Commit();
    }

    private void Validate(RequestTaskJson request)
    {
        var validator = new ValidateTask();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
