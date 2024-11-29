using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using FluentAssertions;
using TaskManagement.Application.UseCases.Login;
using TaskManagement.Domain.Entities;

namespace UseCases.Tests.Users.Login;
public class DoLoginUseCaseTest
{
    [Fact]
    public async System.Threading.Tasks.Task Success()
    {
        var user = UserBuilder.Build();

        var request = RequestLoginJsonBuilder.Build();
        request.Password = "20100119Bb@!";
        request.Email = user.Email;
        var useCase = CreateUseCase(user, request.Password);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(user.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    private DoLoginUserUseCase CreateUseCase(User user, string password)
    {
        var passwordEncripter = new PasswordEncripterBuilder().Verify(password).Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();
        

        return new DoLoginUserUseCase(readRepository, passwordEncripter, tokenGenerator);
    }
}
