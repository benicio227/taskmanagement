using TaskManagement.Domain.Repositories;
using TaskManagement.Domain.Repositories.Tasks;
using TaskManagement.Domain.Services.LoggedUser;
using TaskManagement.Exception;
using TaskManagement.Exception.ExceptionsBase;

namespace TaskManagement.Application.UseCases.Tasks.Delete;
public class DeleteTaskUseCase : IDeleteTaskUseCase
{
    private readonly ITasksReadOnlyRepository _expensesReadOnly;
    private readonly ITasksWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    public DeleteTaskUseCase(
        ITasksWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser,
        ITasksReadOnlyRepository expensesReadOnly)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _expensesReadOnly = expensesReadOnly;

    }
    public async System.Threading.Tasks.Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var task = await _expensesReadOnly.GetById(loggedUser, id);

        if (task is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TASK_WITH_ID_WAS_NOT_FOUND);
        }

        await _repository.Delete(id);
        await _unitOfWork.Commit();
    }
}
