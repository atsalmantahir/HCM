using HumanResourceManagement.Application.Allowances.Commands.Create;
using HumanResourceManagement.Application.Allowances.Queries.Get;
using HumanResourceManagement.Application.Allowances.Queries.List;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AllowancesController : ControllerBase
{
    private readonly IMediator mediator;

    public AllowancesController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IResult> CreateAllowanceAsync([FromBody] CreateAllowanceCommand reqeust) 
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        var response = await mediator.Send(reqeust);
        return Results.Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{externalIdentifier}")]
    public async Task<IResult> GetAllowanceAsync(string externalIdentifier)
    {
        var response = await mediator.Send(new GetAllowanceQuery(externalIdentifier));
        return Results.Ok(response);
    }

    [HttpGet]
    public async Task<IResult> GetAllowancesAsync([FromQuery] GetAllowancesQuery query)
    {
        var response = await mediator.Send(query);
        return Results.Ok(response);
    }
}
