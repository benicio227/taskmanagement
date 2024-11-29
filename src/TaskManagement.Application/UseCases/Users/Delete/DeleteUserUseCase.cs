using TaskManagement.Domain.Repositories;
using TaskManagement.Domain.Repositories.Users;

namespace TaskManagement.Application.UseCases.Users.Delete;
public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly IUserWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteUserUseCase(
        IUserWriteOnlyRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async System.Threading.Tasks.Task Execute(long id)
    {
       await _repository.Delete(id);
       await _unitOfWork.Commit();
    }
}
