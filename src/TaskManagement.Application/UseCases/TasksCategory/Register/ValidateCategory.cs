using FluentValidation;
using TaskManagement.Communication.Requests;
using TaskManagement.Exception;

namespace TaskManagement.Application.UseCases.TasksCategory.Register;
public class ValidateCategory : AbstractValidator<RequestCategoryJson>
{
    public ValidateCategory()
    {
        RuleFor(category => category.Name).NotEmpty().WithMessage(ResourceErrorMessages.THE_NAME_IS_REQUIRED_);
        RuleFor(category => category.Description);
    }
}
