using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;
using TaskManagement.Domain.Repositories.Users;
using TaskManagement.Domain.Security.Cryptography;
using TaskManagement.Domain.Security.Tokens;
using TaskManagement.Exception;
using TaskManagement.Exception.ExceptionsBase;

namespace TaskManagement.Application.UseCases.Login;
public class DoLoginUserUseCase : IDoLoginUserUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUserUseCase(
        IUserReadOnlyRepository userReadOnlyRepository,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = userReadOnlyRepository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }
    public async Task<ResponseLoginUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetUserByEmail(request.Email);

        if (user == null)
        {
            throw new InvalidLoginException(ResourceErrorMessages.INVALID_EMAIL_AND_OR_PASSWORD);
        }

        var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);

        if (passwordMatch == false)
        {
            throw new InvalidLoginException(ResourceErrorMessages.INVALID_EMAIL_AND_OR_PASSWORD);
        }

        return new ResponseLoginUserJson
        {
            Name = user.Name,
            Token = _accessTokenGenerator.Generate(user)
        };
    }
}
