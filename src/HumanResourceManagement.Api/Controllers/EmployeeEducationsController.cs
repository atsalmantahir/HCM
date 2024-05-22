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
        string employeeExternalIdentifier,
        [FromBody] CreateEmployeeEducationCommand request)
    {
        if (employeeExternalIdentifier != request.EmployeeProfile.ExternalIdentifier)
        {
            throw new BadRequestException("Employee External Identifier not match");
        }

        var response = await mediator.Send(request.StructureRequest(employeeExternalIdentifier));
        return Results.Ok(response);
    }

    [Authorize]

    [HttpPut]
    [Route("{employeeExternalIdentifier}/education/{externalIdentifier}")]
    public async Task<IResult> UpdateEmployeeEducationAsync(
        string employeeExternalIdentifier,
        string externalIdentifier,
        [FromBody] UpdateEmployeeExperienceCommand request)
    {
        if (employeeExternalIdentifier != request?.EmployeeProfile?.ExternalIdentifier)
        {
            throw new BadRequestException("Employee External Identifier not match");
        }

        if (externalIdentifier != request?.ExternalIdentifier)
        {
            throw new BadRequestException("External Identifier not match");
        }

        var response = await mediator.Send(request.StructureRequest(employeeExternalIdentifier, externalIdentifier));
        return Results.Ok(response);
    }

    [Authorize]

    [HttpDelete]
    [Route("{employeeExternalIdentifier}/education/{externalIdentifier}")]
    public async Task<IResult> DeleteEmployeeEducationAsync(
        string employeeExternalIdentifier,
        string externalIdentifier)
    {
        var response = await mediator.Send(new DeleteEmployeeEducationCommand(employeeExternalIdentifier, externalIdentifier));
        return Results.Ok(response);
    }

    [Authorize]

    [HttpGet]
    [Route("{employeeExternalIdentifier}/education/{externalIdentifier}")]
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
        string employeeExternalIdentifier,
        string externalIdentifier)
    {
        var response = await mediator.Send(new GetEmployeeEducationQuery(employeeExternalIdentifier, externalIdentifier));
        return Results.Ok(response);
    }
}
