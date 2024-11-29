using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using FluentAssertions;
using TaskManagement.Application.UseCases.Users.Register;
using TaskManagement.Exception;
using TaskManagement.Exception.ExceptionsBase;

namespace UseCases.Tests.Users.Register;
public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestUserJsonBuilder.Build();
        request.Password = "20100119Bb@!";
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);

    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestUserJsonBuilder.Build();
        request.Name = string.Empty;
        request.Password = "20100119Bb@!";

        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.Errors.Count == 1 && ex.Errors.Contains(ResourceErrorMessages.THE_NAME_IS_REQUIRED_));
    }

    [Fact]
    public async Task Error_Email_Already_Exists()
    {
        var request = RequestUserJsonBuilder.Build();
        request.Password = "20100119Bb@!";

        var useCase = CreateUseCase(request.Email);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.Errors.Count == 1 && ex.Errors.Contains(ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
    }

    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
        var passwordEncripter = new PasswordEncripterBuilder().Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder();

        if (string.IsNullOrWhiteSpace(email) == false)
        {
            readRepository.ExistActiveUserWithEmail(email);
        }

        return new RegisterUserUseCase(writeRepository, unitOfWork, mapper, passwordEncripter, readRepository.Build(), tokenGenerator);
    }


}
