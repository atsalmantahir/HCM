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
    [Route("organisations/{organisationExternalIdentifier}/departments")]
    public async Task<IResult> CreateDepartmentAsync(
        string organisationExternalIdentifier,
        [FromBody] CreateDepartmentCommand createDepartmentCommand)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        if (organisationExternalIdentifier != createDepartmentCommand.Organisation.ExternalIdentifier)
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
    /// <param name="externalIdentifier"></param>
    /// <returns></returns>

    [HttpGet]
    [Route("organisations/{organisationExternalIdentifier}/departments/{externalIdentifier}")]

    public async Task<IResult> GetDepartmentAsync(
        string organisationExternalIdentifier,
        string externalIdentifier)
    {
        var response = await mediator.Send(new GetDepartmentQuery(organisationExternalIdentifier, externalIdentifier));
        return Results.Ok(response);
    }

    /// <summary>
    /// get department
    /// </summary>
    /// <param name="organisationExternalIdentifier"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("organisations/{organisationExternalIdentifier}/departments")]

    public async Task<IResult> GetDepartmentsAsync(
        string organisationExternalIdentifier,
        [FromQuery] GetDepartmentsQuery query)
    {

        var response = await mediator.Send(new GetDepartmentsQuery(organisationExternalIdentifier));
        return Results.Ok(response);
    }

    /// <summary>
    /// update department
    /// </summary>
    /// <param name="organisationExternalIdentifier"></param>
    /// <param name="externalIdentifier"></param>
    /// <param name="updateDepartmentCommand"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("organisations/{organisationExternalIdentifier}/departments/{externalIdentifier}")]
    public async Task<IResult> UpdateDepartmentAsync(
        string organisationExternalIdentifier,
        string externalIdentifier,
        [FromBody] UpdateDepartmentCommand updateDepartmentCommand)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        if (organisationExternalIdentifier != updateDepartmentCommand.Organisation.ExternalIdentifier)
        {
            return Results.BadRequest("Organistaion External Identifier mis match!");
        }

        if (externalIdentifier != updateDepartmentCommand.ExternalIdentifier) 
        {
            return Results.BadRequest();
        }

        var response = await mediator.Send(updateDepartmentCommand);
        return Results.Ok(response);
    }

    /// <summary>
    /// delete department
    /// </summary>
    /// <param name="externalIdentifier"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("organisations/{organisationExternalIdentifier}/departments/{externalIdentifier}")]
    public async Task<IResult> DeleteDepartmentAsync(
        string organisationExternalIdentifier,
        string externalIdentifier)
    {
        var response = await mediator.Send(new DeleteDepartmentCommand(organisationExternalIdentifier, externalIdentifier));
        return Results.NoContent();
    }
}
