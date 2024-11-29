using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.UseCases.TasksCategory.Delete;
using TaskManagement.Application.UseCases.TasksCategory.GetAll;
using TaskManagement.Application.UseCases.TasksCategory.GetById;
using TaskManagement.Application.UseCases.TasksCategory.Register;
using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;

namespace TaskManagement.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaskCategoryController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(RequestCategoryJson request, [FromServices] IRegisterTaskCategoryUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllCategorys([FromServices] IGetAllCategoryUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    
    public async Task<IActionResult> GetById([FromRoute] long id, [FromServices] IGetByIdCategoryUseCase useCase)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete([FromRoute] long id, [FromServices] IDeleteByIdTaskCategoryUseCase useCase)
    {
        await useCase.Execute(id);

        return Ok();
    }
}
