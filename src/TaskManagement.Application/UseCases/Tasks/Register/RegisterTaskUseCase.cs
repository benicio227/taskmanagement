using AutoMapper;
using TaskManagement.Application.UseCases.Task.Register;
using TaskManagement.Application.UseCases.Tasks.Register;
using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;

using TaskManagement.Domain.Repositories;
using TaskManagement.Domain.Repositories.Tasks;
using TaskManagement.Domain.Services.LoggedUser;
using TaskManagement.Exception;
using TaskManagement.Exception.ExceptionsBase;

namespace TaskManagement.Application.UseCases.TasksManagement.Register;
public class RegisterTaskUseCase : IRegisterTaskUseCase
{
    private readonly ITasksWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public RegisterTaskUseCase(
        ITasksWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseTaskJson> Execute(RequestTaskJson request)
    {
       
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var pendingTasksCount = await _repository.GetPendingTasksCount(loggedUser.Id);

        const int maxPendingTasks = 10;

        if (pendingTasksCount >= maxPendingTasks)
        {
           throw new ErrorOnValidationException(new List<string> {ResourceErrorMessages.PENDING_TASK_LIMIT_RECHEADED});
        }

        //Aqui estamos mapeando as propriedades do objeto "request"(RequestTaskJson) para o objeto/entidade "Task"
        var task = _mapper.Map<Domain.Entities.Task>(request);
        task.UserId = loggedUser.Id;

        //Aqui adicionamos a entidade "Task" já com as propriedades preenchidas no banco de dados
        await _repository.Add(task);

        //Aqui salvamos
        await _unitOfWork.Commit();

        //Aqui mapeamos as propriedades da entidade "Task" para o obeto DTO de resposta "ResponseTaskJson"
        return _mapper.Map<ResponseTaskJson>(task);
    }

    private void Validate(RequestTaskJson request)
    {
        var validate = new ValidateTask();

        var result = validate.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
