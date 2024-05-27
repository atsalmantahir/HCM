using HumanResourceManagement.Application.EmployeeEducations.Commands.Create;
using HumanResourceManagement.Application.EmployeeEducations.Commands.Delete;
using HumanResourceManagement.Application.EmployeeEducations.Queries.Get;
using HumanResourceManagement.Application.EmployeeEducations.Queries.List;
using HumanResourceManagement.Application.EmployeeExperiences.Commands.Update;
using HumanResourceManagement.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Api.Controllers;

[Route("api/employees/profile")]
[ApiController]
public class EmployeeEducationsController : ControllerBase
{
    private readonly IMediator mediator;

    public EmployeeEducationsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Authorize]

    [HttpPost]
    [Route("{employeeExternalIdentifier}/education")]
    public async Task<IResult> CreateEmployeeEducationAsync(
        int employeeExternalIdentifier,
        [FromBody] CreateEmployeeEducationCommand request)
    {
        if (employeeExternalIdentifier != request.EmployeeProfile.Id)
        {
            throw new BadRequestException("Employee External Identifier not match");
        }

        var response = await mediator.Send(request.StructureRequest(employeeExternalIdentifier));
        return Results.Ok(response);
    }

    [Authorize]

    [HttpPut]
    [Route("{employeeExternalIdentifier}/education/{id}")]
    public async Task<IResult> UpdateEmployeeEducationAsync(
        int employeeExternalIdentifier,
        int id,
        [FromBody] UpdateEmployeeExperienceCommand request)
    {
        if (employeeExternalIdentifier != request?.EmployeeProfile?.Id)
        {
            throw new BadRequestException("Employee External Identifier not match");
        }

        if (id != request?.Id)
        {
            throw new BadRequestException("External Identifier not match");
        }

        var response = await mediator.Send(request.StructureRequest(employeeExternalIdentifier, id));
        return Results.Ok(response);
    }

    [Authorize]

    [HttpDelete]
    [Route("{employeeExternalIdentifier}/education/{id}")]
    public async Task<IResult> DeleteEmployeeEducationAsync(
        int employeeExternalIdentifier,
        int id)
    {
        var response = await mediator.Send(new DeleteEmployeeEducationCommand(employeeExternalIdentifier, id));
        return Results.Ok(response);
    }

    [Authorize]

    [HttpGet]
    [Route("{employeeExternalIdentifier}/education/{id}")]
    public async Task<IResult> ListEmployeeEducationAsync(
        string employeeExternalIdentifier,
        [FromQuery] GetEmployeeEducationsQuery query)
    {
        var response = await mediator.Send(query);
        return Results.Ok(response);
    }

    [Authorize]

    [HttpGet]
    [Route("{employeeExternalIdentifier}/education")]
    public async Task<IResult> GetEmployeeEducationAsync(
        int employeeExternalIdentifier,
        int id)
    {
        var response = await mediator.Send(new GetEmployeeEducationQuery(employeeExternalIdentifier, id));
        return Results.Ok(response);
    }
}
