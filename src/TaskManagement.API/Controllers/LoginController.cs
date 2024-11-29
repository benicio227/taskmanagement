using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.UseCases.Login;
using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;

namespace TaskManagement.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    //OBS: Usamos Post porque para informações sensíveis, é melhor usar o método POST para enviar no corpo da requisição
    [HttpPost]
    [ProducesResponseType(typeof(ResponseLoginUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]

    public async Task<IActionResult> Login([FromBody] RequestLoginJson request, [FromServices] IDoLoginUserUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
