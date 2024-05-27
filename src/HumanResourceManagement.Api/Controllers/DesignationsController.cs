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
    /// <param name="departmentId"></param>
    /// <param name="createDesignationCommand"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("departments/{departmentId}/designations")]
    public async Task<IResult> CreateDesignationAsync(
        int departmentId,
        [FromBody] CreateDesignationCommand createDesignationCommand)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        if (departmentId != createDesignationCommand.Department.Id)
        {
            return Results.BadRequest("Organistaion External Identifier mis match!");
        }

        var response = await mediator.Send(createDesignationCommand);
        return Results.Created(string.Empty, response);
    }

    /// <summary>
    /// get designation
    /// </summary>
    /// <param name="departmentId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("departments/{departmentId}/designations/{id}")]
    public async Task<IResult> GetDesignationAsync(
        int departmentId,
        int id)
    {
        var response = await mediator.Send(new GetDesignationQuery(departmentId, id));
        return Results.Ok(response);
    }

    /// <summary>
    /// get department
    /// </summary>
    /// <param name="departmentId"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("departments/{departmentId}/designations")]
    public async Task<IResult> GetDesignationsAsync(int departmentId, [FromQuery] GetDesignationsQuery query)
    {
        //Log.Information("Get de");

        var response = await mediator.Send(new GetDesignationsQuery(departmentId));
        return Results.Ok(response);
    }

    /// <summary>
    /// update department
    /// </summary>
    /// <param name="departmentId"></param>
    /// <param name="id"></param>
    /// <param name="updateDesignationCommand"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("departments/{departmentId}/designations/{id}")]
    public async Task<IResult> UpdateDesignationAsync(
        int departmentId,
        int id,
        [FromBody] UpdateDesignationCommand updateDesignationCommand)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        if (departmentId != updateDesignationCommand.Department.Id)
        {
            return Results.BadRequest("Organistaion External Identifier mis match!");
        }

        if (id != updateDesignationCommand.Id)
        {
            return Results.BadRequest();
        }

        var response = await mediator.Send(updateDesignationCommand);
        return Results.Ok(response);
    }
}
