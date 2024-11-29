using FluentValidation;
using TaskManagement.Communication.Requests;
using TaskManagement.Exception;

namespace TaskManagement.Application.UseCases.Users.Register;
public class ValidateUsers : AbstractValidator<RequestUserJson>
{
    public ValidateUsers()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceErrorMessages.THE_NAME_IS_REQUIRED_);
        RuleFor(user => user.Email)
            .Cascade(CascadeMode.Stop) //Esse método Interrompe se houver uma falha no método .NotEmpty();
                                      //Ou seja, se não for passado nenhum email, ele não prossegue com a validação do EmailAdress()
                                      //Esse foi um desafio que tive que resolver para poder validar no teste de unidade
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.THE_EMAIL_IS_REQUIRED)
            .EmailAddress()
            .WithMessage(ResourceErrorMessages.THE_EMAIL_IS_NOT_IN_VALID_FORMAT);

        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestUserJson>());
    }
}
