using CommonTestUtilities.Requests;
using FluentAssertions;
using TaskManagement.Application.UseCases.TasksCategory.Register;
using TaskManagement.Exception;

namespace Validators.Tests.TasksCategory.Register;
public class RegisterTaskCategoryValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new ValidateCategory();
        var request = RequestTaskCategoryJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Error_Name_Empty(string name)
    {
        var validate = new ValidateCategory();
        var request = RequestTaskCategoryJsonBuilder.Build();
        request.Name = name;

        var result = validate.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.THE_NAME_IS_REQUIRED_));
    }
}
