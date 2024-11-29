
using TaskManagement.Domain.Repositories;
using TaskManagement.Domain.Repositories.TasksCategory;
using TaskManagement.Domain.Services.LoggedUser;

namespace TaskManagement.Application.UseCases.TasksCategory.Delete;
public class DeleteByIdTaskCategoryUseCase : IDeleteByIdTaskCategoryUseCase
{
    private ITasksCategoryWriteOnlyRepository _repository;
    private ILoggedUser _loggedUser;
    private IUnitOfWork _unitOfWork;
    public DeleteByIdTaskCategoryUseCase(
        ITasksCategoryWriteOnlyRepository repository,
        ILoggedUser loggedUser,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
    }

    public async System.Threading.Tasks.Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        await _repository.Delete(loggedUser, id);


        await _unitOfWork.Commit();
    }
}
