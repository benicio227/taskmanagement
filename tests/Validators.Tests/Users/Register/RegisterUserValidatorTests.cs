using CommonTestUtilities.Requests;
using FluentAssertions;
using TaskManagement.Application.UseCases.Users.Register;
using TaskManagement.Exception;

namespace Validators.Tests.Users.Register;
public class RegisterUserValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new ValidateUsers();
        var request = RequestUserJsonBuilder.Build();
        request.Password = "20100119Bb@!";

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();   

    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Error_Name_Empty(string name)
    {
        var validator = new ValidateUsers();
        var request = RequestUserJsonBuilder.Build();
        request.Name = name;
        request.Password = "20100119Bb@!";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.THE_NAME_IS_REQUIRED_));
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_Email_Empty(string email)
    {
        var validator = new ValidateUsers();
        var request = RequestUserJsonBuilder.Build();
        request.Email = email;
        request.Password = "20100119Bb@!";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.THE_EMAIL_IS_REQUIRED));
    }

    [Theory]
    [InlineData("")]
    [InlineData("12345678")]
    [InlineData("abcdefghij")]
    [InlineData("ABCDEFGHIJ")]
    [InlineData("abcd1234")]
    [InlineData("Abcd1234")]

    public void Password_Invalid(string password)
    {
        var validator = new ValidateUsers();
        var request = RequestUserJsonBuilder.Build();
        request.Password = password;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PASSWORD_INVALID));
    }
}
