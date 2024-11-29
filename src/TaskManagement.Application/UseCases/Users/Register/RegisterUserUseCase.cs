using AutoMapper;
using FluentValidation.Results;
using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using TaskManagement.Domain.Repositories.Users;
using TaskManagement.Domain.Security.Cryptography;
using TaskManagement.Domain.Security.Tokens;
using TaskManagement.Exception;
using TaskManagement.Exception.ExceptionsBase;

namespace TaskManagement.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IAccessTokenGenerator _tokenGenerator;
    public RegisterUserUseCase(
        IUserWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IPasswordEncripter passwordEncripter,
        IUserReadOnlyRepository userReadOnlyRepository,
        IAccessTokenGenerator tokenGenerator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _tokenGenerator = tokenGenerator;
        
    }
    public async Task<ResponseUserJson> Execute(RequestUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);

        await _repository.Add(user);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseUserJson>(user);

    }

    private async System.Threading.Tasks.Task Validate(RequestUserJson request)
    {
        var validator = new ValidateUsers();
        var result = validator.Validate(request);

        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(erro => erro.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
