using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.UseCases.Tasks.Delete;
using TaskManagement.Application.UseCases.Tasks.GetAll;
using TaskManagement.Application.UseCases.Tasks.GetById;
using TaskManagement.Application.UseCases.Tasks.Register;
using TaskManagement.Application.UseCases.Tasks.Update;
using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;

namespace TaskManagement.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize] //O atributo Authorize vai garantir que todo endpoint dentro desse controller só vai ser chamado,
            //se esta chamada enviar para gente um token válido. É através do token que precisamo recuperar o id do
            //usuário.
public class TaskController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseTaskJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RequestTaskJson request, [FromServices] IRegisterTaskUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseTasksJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<IActionResult> GetAllTasks([FromServices] IGetAllTaskUseCase useCase)
    {
        var response = await useCase.Execute();
        
        if(response == null || !response.Tasks.Any())
        {
            return NoContent();
        }

        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseTaskJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetById([FromRoute] long id, [FromServices] IGetByIdTaskUseCase useCase)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Update([FromRoute] long id, [FromBody] RequestTaskJson request, [FromServices] IUpdateTaskUseCase useCase)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete([FromRoute] long id, [FromServices] IDeleteTaskUseCase useCase)
    {
        await useCase.Execute(id);

        return NoContent();
    }
}
