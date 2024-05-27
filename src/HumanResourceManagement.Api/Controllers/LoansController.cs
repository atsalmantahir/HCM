using HumanResourceManagement.Application.EmployeeLoans.Commands.Create;
using HumanResourceManagement.Application.EmployeeLoans.Queries.Get;
using HumanResourceManagement.Application.EmployeeLoans.Queries.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoansController : ControllerBase
{
    private readonly IMediator mediator;

    public LoansController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public async Task<IResult> CreateEmployeeLoanAsync(
        [FromBody] CreateEmployeeLoanCommand createEmployeeLoanCommand)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        var response = await mediator.Send(createEmployeeLoanCommand);
        return Results.Created(string.Empty, response);
    }

    [Authorize]
    [HttpGet]
    public async Task<IResult> GetEmployeeLoansAsync([FromQuery] GetEmployeeLoansQuery query) 
    {
        var response = await mediator.Send(query).ConfigureAwait(false);
        return Results.Ok(response);
    }

    [Authorize]
    [HttpGet]
    [Route("{id}")]
    public async Task<IResult> GetEmployeeLoanAsync(int id)
    {
        var response = await mediator.Send(new GetEmployeeLoanQuery(id)).ConfigureAwait(false);
        return Results.Ok(response);
    }
}
