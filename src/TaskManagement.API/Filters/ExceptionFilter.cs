using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManagement.Communication.Responses;
using TaskManagement.Exception;
using TaskManagement.Exception.ExceptionsBase;

namespace TaskManagement.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is TaskManagementException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknowError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        if(context.Exception is ErrorOnValidationException)
        {
            var ex = (ErrorOnValidationException)context.Exception;

            var errorResponse = new ResponseErrorJson(ex.Errors);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorResponse);
        }
        else if(context.Exception is NotFoundException)
        {
            var ex = (NotFoundException)context.Exception;

            var errorResponse = new ResponseErrorJson(ex.Error);

            context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Result = new ObjectResult(errorResponse);
        }
        else if (context.Exception is InvalidLoginException)
        {
            var ex = (InvalidLoginException)context.Exception;

            var errorResponse = new ResponseErrorJson(ex.Error);

            context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Result = new ObjectResult(errorResponse);
        }
    }

    private void ThrowUnknowError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOW_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
