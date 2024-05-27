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
    [Route("{employeeId}/compensation")]
    public async Task<IResult> CreateEmployeeCompensationAsync(
        int employeeId,
        [FromBody] CreateEmployeeCompensationCommand request)
    {
        if (employeeId != request?.EmployeeProfile.Id)
        {
            throw new BadRequestException("Employee profile external identifier did not matched");
        }

        var response = await mediator.Send(request.StructureRequest(employeeId));
        return Results.Created(string.Empty, response);
    }

    [Authorize]
    [HttpPut]
    [Route("{employeeId}/compensation/{id}")]
    public async Task<IResult> UpdateEmployeeCompensationAsync(
        int id,
        int employeeId,
        [FromBody] UpdateEmployeeCompensationCommand request)
    {
        if (id != request?.EmployeeCompenstaionId)
        {
            throw new BadRequestException("External Identifier not match");
        }

        if (employeeId != request?.EmployeeProfile.Id)
        {
            throw new BadRequestException("Employee External Identifier not match");
        }

        var response = await mediator.Send(request.StructureRequest());
        return Results.Ok(response);
    }

    [Authorize]
    [HttpDelete]
    [Route("{employeeId}/compensation/{id}")]
    public async Task<IResult> DeleteEmployeeCompensationAsync(
        int employeeId,
        int id)
    {
        var response = await mediator.Send(new DeleteEmployeeCompensationCommand(employeeId, id));
        return Results.Ok(response);
    }

    [Authorize]
    [HttpGet]
    [Route("{employeeId}/compensation")]
    public async Task<IResult> ListEmployeeCompensationAsync(
        string employeeId,
        [FromQuery] GetEmployeeCompensationsQuery query)
    {
        var response = await mediator.Send(query);
        return Results.Ok(response);
    }

    [Authorize]
    [HttpGet]
    [Route("{employeeId}/compensation/{id}")]
    public async Task<IResult> GetEmployeeCompensationAsync(
        int employeeId,
        int id)
    {
        var response = await mediator.Send(new GetEmployeeCompensationQuery(employeeId, id));
        return Results.Ok(response);
    }

}
