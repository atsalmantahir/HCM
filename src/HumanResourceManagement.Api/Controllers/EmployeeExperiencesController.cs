using HumanResourceManagement.Application.EmployeeExperiences.Commands.Create;
using HumanResourceManagement.Application.EmployeeExperiences.Commands.Delete;
using HumanResourceManagement.Application.EmployeeExperiences.Commands.Update;
using HumanResourceManagement.Application.EmployeeExperiences.Queries.Get;
using HumanResourceManagement.Application.EmployeeExperiences.Queries.List;
using HumanResourceManagement.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Api.Controllers;

[Route("api/employees/profile")]
[ApiController]
public class EmployeeExperiencesController : ControllerBase
{
    private readonly IMediator mediator;
    public EmployeeExperiencesController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [Authorize]
    [HttpPost]
    [Route("{employeeExternalIdentifier}/experience")]
    public async Task<IResult> CreateEmployeeExperienceAsync(
        int employeeExternalIdentifier,
        [FromBody] CreateEmployeeExperienceCommand request)
    {
        if (employeeExternalIdentifier != request?.EmployeeProfile?.Id)
        {
            return Results.BadRequest("Unique Identifier not match");
        }

        var response = await mediator.Send(request.StructureRequest(employeeExternalIdentifier));
        return Results.Ok(response);
    }

    [Authorize]

    [HttpPut]
    [Route("{employeeExternalIdentifier}/experience/{id}")]
    public async Task<IResult> UpdateEmployeeExperienceAsync(
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
    [Route("{employeeExternalIdentifier}/experience/{id}")]
    public async Task<IResult> DeleteEmployeeExperienceAsync(
        int employeeExternalIdentifier,
        int id)
    {
        var response = await mediator.Send(new DeleteEmployeeExperienceCommand(employeeExternalIdentifier, id));
        return Results.Ok(response);
    }

    [Authorize]

    [HttpGet]
    [Route("{employeeExternalIdentifier}/experience/{id}")]
    public async Task<IResult> ListEmployeeExperienceAsync(
        int employeeExternalIdentifier,
        [FromQuery] GetEmployeeExperiencesQuery query)
    {
        var response = await mediator.Send(query);
        return Results.Ok(response);
    }

    [Authorize]

    [HttpGet]
    [Route("{employeeExternalIdentifier}/experience")]
    public async Task<IResult> GetEmployeeExperienceAsync(
        int employeeExternalIdentifier,
        int id)
    {
        var response = await mediator.Send(new GetEmployeeExperienceQuery(employeeExternalIdentifier, id));
        return Results.Ok(response);
    }
}
