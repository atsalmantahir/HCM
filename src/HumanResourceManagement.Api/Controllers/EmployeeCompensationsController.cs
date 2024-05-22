using HumanResourceManagement.Application.EmployeeCompensations.Commands.Create;
using HumanResourceManagement.Application.EmployeeCompensations.Commands.Delete;
using HumanResourceManagement.Application.EmployeeCompensations.Queries.Get;
using HumanResourceManagement.Application.EmployeeCompensations.Queries.List;
using HumanResourceManagement.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Api.Controllers;

[Route("api/employees/profile")]
[ApiController]
public class EmployeeCompensationsController : ControllerBase
{
    private readonly IMediator mediator;
    public EmployeeCompensationsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Authorize]

    [HttpPost]
    [Route("{employeeExternalIdentifier}/compensation")]
    public async Task<IResult> CreateEmployeeCompensationAsync(
        string employeeExternalIdentifier,
        [FromBody] CreateEmployeeCompensationCommand request)
    {
        if (employeeExternalIdentifier != request?.EmployeeProfile?.ExternalIdentifier)
        {
            throw new BadRequestException("Employee profile external identifier did not matched");
        }

        var response = await mediator.Send(request.StructureRequest(employeeExternalIdentifier));
        return Results.Created(string.Empty, response);
    }

    [Authorize]

    [HttpPut]
    [Route("{employeeExternalIdentifier}/compensation/{externalIdentifier}")]
    public async Task<IResult> UpdateEmployeeCompensationAsync(
        string externalIdentifier,
        string employeeExternalIdentifier,
        [FromBody] UpdateEmployeeCompensationCommand request)
    {
        if (externalIdentifier != request?.ExternalIdentifier)
        {
            throw new BadRequestException("External Identifier not match");
        }

        if (employeeExternalIdentifier != request?.EmployeeProfile?.ExternalIdentifier)
        {
            throw new BadRequestException("Employee External Identifier not match");
        }

        var response = await mediator.Send(request.StructureRequest(externalIdentifier, employeeExternalIdentifier));
        return Results.Ok(response);
    }

    [Authorize]

    [HttpDelete]
    [Route("{employeeExternalIdentifier}/compensation/{externalIdentifier}")]
    public async Task<IResult> DeleteEmployeeCompensationAsync(
        string employeeExternalIdentifier,
        string externalIdentifier)
    {
        var response = await mediator.Send(new DeleteEmployeeCompensationCommand(employeeExternalIdentifier, externalIdentifier));
        return Results.Ok(response);
    }

    [Authorize]

    [HttpGet]
    [Route("{employeeExternalIdentifier}/compensation")]
    public async Task<IResult> ListEmployeeCompensationAsync(
        string employeeExternalIdentifier,
        [FromQuery] GetEmployeeCompensationsQuery query)
    {
        var response = await mediator.Send(query);
        return Results.Ok(response);
    }

    [Authorize]

    [HttpGet]
    [Route("{employeeExternalIdentifier}/compensation/{externalIdentifier}")]
    public async Task<IResult> GetEmployeeCompensationAsync(
        string employeeExternalIdentifier,
        string externalIdentifier)
    {
        var response = await mediator.Send(new GetEmployeeCompensationQuery(employeeExternalIdentifier, externalIdentifier));
        return Results.Ok(response);
    }

}
