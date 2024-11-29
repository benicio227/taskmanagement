using CommonTestUtilities.Requests;
using FluentAssertions;
using TaskManagement.Application.UseCases.Task.Register;
using TaskManagement.Communication.Enums;
using TaskManagement.Exception;

namespace Validators.Tests.Tasks.Register;
public class RegisterTaskValidatorTests
{
    [Fact]
    public void Success()
    {
        //Arrange - É a parte onde vamos criar as instâncias de tudo que precisamos para testar
        var validator = new ValidateTask();
        var request = RequestTaskJsonBuilder.Build();  //Fazer dessa forma vai gerar 2 problemas: O código vai ficar fixo, os dados serão os mesmos
                                           
        //Act - Aqui pegamos o validator e falamos: Validator, valida essa requisição
        var result = validator.Validate(request);

        //Assert - Nessa função de Success esperamos que a resposta do result seja True
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        var validator = new ValidateTask();
        var request = RequestTaskJsonBuilder.Build();
        request.Title = title;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.THE_TITLE_IS_REQUIRED));
    }

    [Fact]
    public void Error_Date_Future()
    {
        var validator = new ValidateTask();
        var request = RequestTaskJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.THE_DATE_CANNOT_BE_IN_THE_FUTURE));
    }

    [Fact]
    public void Error_TypeTask_Invalid()
    {
        var validator = new ValidateTask();
        var request = RequestTaskJsonBuilder.Build();
        request.Type = (TaskType)6;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.THE_TASK_TYPE_MUST_BE_A_VALID_ENUM_VALUE));
    }
}
