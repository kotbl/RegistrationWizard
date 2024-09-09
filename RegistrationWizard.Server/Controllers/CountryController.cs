using MediatR;
using Microsoft.AspNetCore.Mvc;
using RegistrationWizard.Application.Countries.Queries.List;

namespace RegistrationWizard.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class CountryController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await mediator.Send(new ListCountryQuery());

        return Ok(result);
    }
}
