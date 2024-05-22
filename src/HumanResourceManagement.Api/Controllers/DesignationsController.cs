using HumanResourceManagement.Application.Departments.Commands.Create;
using HumanResourceManagement.Application.Departments.Commands.Update;
using HumanResourceManagement.Application.Departments.Queries.Get;
using HumanResourceManagement.Application.Departments.Queries.List;
using HumanResourceManagement.Application.Designations.Commands.Create;
using HumanResourceManagement.Application.Designations.Commands.Update;
using HumanResourceManagement.Application.Designations.Queries.Get;
using HumanResourceManagement.Application.Designations.Queries.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HumanResourceManagement.Api.Controllers;

/// <summary>
/// designations controller
/// </summary>

[Authorize]
[Route("api/")]
[ApiController]
public class DesignationsController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="mediator"></param>
    public DesignationsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// create department
    /// </summary>
    /// <param name="departmentExternalIdentifier"></param>
    /// <param name="createDesignationCommand"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("departments/{departmentExternalIdentifier}/designations")]
    public async Task<IResult> CreateDesignationAsync(
        string departmentExternalIdentifier,
        [FromBody] CreateDesignationCommand createDesignationCommand)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        if (departmentExternalIdentifier != createDesignationCommand.Department.ExternalIdentifier)
        {
            return Results.BadRequest("Organistaion External Identifier mis match!");
        }

        var response = await mediator.Send(createDesignationCommand);
        return Results.Created(string.Empty, response);
    }

    /// <summary>
    /// get designation
    /// </summary>
    /// <param name="departmentExternalIdentifier"></param>
    /// <param name="externalIdentifier"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("departments/{departmentExternalIdentifier}/designations/{externalIdentifier}")]
    public async Task<IResult> GetDesignationAsync(
        string departmentExternalIdentifier,
        string externalIdentifier)
    {
        var response = await mediator.Send(new GetDesignationQuery(departmentExternalIdentifier, externalIdentifier));
        return Results.Ok(response);
    }

    /// <summary>
    /// get department
    /// </summary>
    /// <param name="departmentExternalIdentifier"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("departments/{departmentExternalIdentifier}/designations")]
    public async Task<IResult> GetDesignationsAsync(string departmentExternalIdentifier, [FromQuery] GetDesignationsQuery query)
    {
        //Log.Information("Get de");

        var response = await mediator.Send(new GetDesignationsQuery(departmentExternalIdentifier));
        return Results.Ok(response);
    }

    /// <summary>
    /// update department
    /// </summary>
    /// <param name="departmentExternalIdentifier"></param>
    /// <param name="externalIdentifier"></param>
    /// <param name="updateDesignationCommand"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("departments/{departmentExternalIdentifier}/designations/{externalIdentifier}")]
    public async Task<IResult> UpdateDesignationAsync(
        string departmentExternalIdentifier,
        string externalIdentifier,
        [FromBody] UpdateDesignationCommand updateDesignationCommand)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        if (departmentExternalIdentifier != updateDesignationCommand.Department.ExternalIdentifier)
        {
            return Results.BadRequest("Organistaion External Identifier mis match!");
        }

        if (externalIdentifier != updateDesignationCommand.ExternalIdentifier)
        {
            return Results.BadRequest();
        }

        var response = await mediator.Send(updateDesignationCommand);
        return Results.Ok(response);
    }
}
