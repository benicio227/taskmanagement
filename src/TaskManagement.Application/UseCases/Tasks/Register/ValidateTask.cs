using FluentValidation;
using TaskManagement.Communication.Requests;
using TaskManagement.Exception;

namespace TaskManagement.Application.UseCases.Task.Register;
public class ValidateTask : AbstractValidator<RequestTaskJson>
{
    public ValidateTask()
    {
        RuleFor(task => task.Title).NotEmpty().WithMessage(ResourceErrorMessages.THE_TITLE_IS_REQUIRED);
        RuleFor(task => task.Description);
        RuleFor(task => task.Date).NotEmpty().WithMessage(ResourceErrorMessages.THE_DATE_CANNOT_BE_IN_THE_FUTURE);
        RuleFor(task => task.Type).IsInEnum().WithMessage(ResourceErrorMessages.THE_TASK_TYPE_MUST_BE_A_VALID_ENUM_VALUE);
    }
}
