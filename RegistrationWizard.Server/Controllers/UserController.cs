using MediatR;
using Microsoft.AspNetCore.Mvc;
using RegistrationWizard.Application.Users.Commands.Create;

namespace RegistrationWizard.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(UserCreateCommand command)
    {
        var userId = await mediator.Send(command);

        return Ok(userId);
    }
}