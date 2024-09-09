using MediatR;
using Microsoft.AspNetCore.Mvc;
using RegistrationWizard.Application.Countries.Queries.List;
using RegistrationWizard.Application.Provinces.List;

namespace RegistrationWizard.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class ProvinceController(IMediator mediator) : ControllerBase
{
    [HttpGet("{countryId:int}")]
    public async Task<IActionResult> Get(int countryId)
    {
        var result = await mediator.Send(new ListProvinceQuery() { CountryId = countryId });

        return Ok(result);
    }
}