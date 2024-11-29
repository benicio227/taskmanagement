using AutoMapper;
using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using TaskManagement.Domain.Repositories.TasksCategory;
using TaskManagement.Domain.Services.LoggedUser;
using TaskManagement.Exception.ExceptionsBase;

namespace TaskManagement.Application.UseCases.TasksCategory.Register;
public class RegisterTaskCategoryUseCase : IRegisterTaskCategoryUseCase
{
    private readonly ITasksCategoryWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public RegisterTaskCategoryUseCase(
        ITasksCategoryWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseTaskCategoryJson> Execute(RequestCategoryJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        //Aqui estou mapeando os dados de request "RequestCategoryJson" para o objeto/entidade "TaskCategory"
        var taskCategory = _mapper.Map<TaskCategory>(request); //Retorna o objeto/entidade TaskCategory
        taskCategory.UserId = loggedUser.Id;

        //Aqui adiciono o objeto/entidade ao banco de dados
        _repository.Add(taskCategory);

        await _unitOfWork.Commit();

        //Aqui mapeio os dados de entity TaskCategory para o objeto DTO de resposta ResponseTaskCategoryJson
        return _mapper.Map<ResponseTaskCategoryJson>(taskCategory);
    }

    private void Validate(RequestCategoryJson request)
    {
        var validate = new ValidateCategory();
        var result = validate.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
