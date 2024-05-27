using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.Departments.Commands.Create;
using HumanResourceManagement.Application.Departments.Commands.Delete;
using HumanResourceManagement.Application.Departments.Commands.Update;
using HumanResourceManagement.Application.Departments.Queries.Get;
using HumanResourceManagement.Application.Departments.Queries.List;
using HumanResourceManagement.Application.Organisations.Commands.Delete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Api.Controllers;

/// <summary>
/// Departments controller
/// </summary>

[Authorize]
[Route("api/")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IExceptionHandler exceptionHandler;
    private readonly IHttpContextAccessor httpContextAccessor;

    /// <summary>
    /// default constructor
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="exceptionHandler"></param>
    /// <param name="httpContextAccessor"></param>
    public DepartmentsController(
        IMediator mediator, 
        IExceptionHandler exceptionHandler,
        IHttpContextAccessor httpContextAccessor)
    {
        this.mediator = mediator;
        this.exceptionHandler = exceptionHandler;
        this.httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// create department
    /// </summary>
    /// <param name="organisationExternalIdentifier"></param>
    /// <param name="createDepartmentCommand"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("organisations/{organisationId}/departments")]
    public async Task<IResult> CreateDepartmentAsync(
        int organisationId,
        [FromBody] CreateDepartmentCommand createDepartmentCommand)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        if (organisationId != createDepartmentCommand.Organisation.Id)
        {
            return Results.BadRequest("Organistaion External Identifier mis match!");
        }

        var response = await mediator.Send(createDepartmentCommand);
        return Results.Created(string.Empty, response);
    }

    /// <summary>
    /// get department
    /// </summary>
    /// <param name="organisationExternalIdentifier"></param>
    /// <param name="id"></param>
    /// <returns></returns>

    [HttpGet]
    [Route("organisations/{organisationId}/departments/{id}")]

    public async Task<IResult> GetDepartmentAsync(
        int organisationId,
        int id)
    {
        var response = await mediator.Send(new GetDepartmentQuery(organisationId, id));
        return Results.Ok(response);
    }

    /// <summary>
    /// get department
    /// </summary>
    /// <param name="organisationExternalIdentifier"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("organisations/{organisationId}/departments")]

    public async Task<IResult> GetDepartmentsAsync(
        int organisationId,
        [FromQuery] GetDepartmentsQuery query)
    {

        var response = await mediator.Send(new GetDepartmentsQuery(organisationId));
        return Results.Ok(response);
    }

    /// <summary>
    /// update department
    /// </summary>
    /// <param name="organisationExternalIdentifier"></param>
    /// <param name="id"></param>
    /// <param name="updateDepartmentCommand"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("organisations/{organisationId}/departments/{id}")]
    public async Task<IResult> UpdateDepartmentAsync(
        int organisationId,
        int id,
        [FromBody] UpdateDepartmentCommand updateDepartmentCommand)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        if (organisationId != updateDepartmentCommand.Organisation.Id)
        {
            return Results.BadRequest("Organistaion External Identifier mis match!");
        }

        if (id != updateDepartmentCommand.Id) 
        {
            return Results.BadRequest();
        }

        var response = await mediator.Send(updateDepartmentCommand);
        return Results.Ok(response);
    }

    /// <summary>
    /// delete department
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("organisations/{organisationId}/departments/{id}")]
    public async Task<IResult> DeleteDepartmentAsync(
        int organisationId,
        int id)
    {
        var response = await mediator.Send(new DeleteDepartmentCommand(organisationId, id));
        return Results.NoContent();
    }
}
