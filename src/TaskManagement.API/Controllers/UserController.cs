using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.UseCases.Users.Delete;
using TaskManagement.Application.UseCases.Users.GetById;
using TaskManagement.Application.UseCases.Users.Register;
using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;
using TaskManagement.Exception.ExceptionsBase;

namespace TaskManagement.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(RequestUserJson request, [FromServices] IRegisterUserUseCase useCase)
    {
        var response = await useCase.Execute(request);


        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById([FromRoute] long id, [FromServices] IGetByIdUserUseCase useCase)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete([FromRoute] long id, [FromServices] IDeleteUserUseCase useCase)
    {
        await useCase.Execute(id);

        return NoContent();
    }

}
