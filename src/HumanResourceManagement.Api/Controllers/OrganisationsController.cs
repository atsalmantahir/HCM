using HumanResourceManagement.Application.Organisations.Commands.Create;
using HumanResourceManagement.Application.Organisations.Commands.Delete;
using HumanResourceManagement.Application.Organisations.Commands.Update;
using HumanResourceManagement.Application.Organisations.Queries.Get;
using HumanResourceManagement.Application.Organisations.Queries.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Api.Controllers;

/// <summary>
/// organisation controller
/// </summary>

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OrganisationsController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// constructor of organisation controller
    /// </summary>
    /// <param name="mediator"></param>
    public OrganisationsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// create organisation
    /// </summary>
    /// <param name="createOrganisationCommand"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResult> CreateOrganisationAsync(
    [FromBody] CreateOrganisationCommand createOrganisationCommand)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        var response = await mediator.Send(createOrganisationCommand);
        return Results.Created(string.Empty, response);
    }

    /// <summary>
    /// get organistaion
    /// </summary>
    /// <param name="externalIdentifier"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{externalIdentifier}")]
    public async Task<OrganisationVM> GetOgranistaionAsync(string externalIdentifier)
    {
        var response = await mediator.Send(new GetOrganisationQuery(externalIdentifier));
        return response;
    }

    /// <summary>
    /// get organistaion list
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IResult> GetOgranistaionsAsync([FromQuery] GetOrganisationsQuery query)
    {
        var response = await mediator.Send(query);
        return Results.Ok(response);
    }

    /// <summary>
    /// update organistaion
    /// </summary>
    /// <param name="externalIdentifier"></param>
    /// <param name="updateOrganisationCommand"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{externalIdentifier}")]
    public async Task<IResult> UpdateOrganisaionAsync(string externalIdentifier,
        [FromBody] UpdateOrganisationCommand updateOrganisationCommand)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        if (externalIdentifier != updateOrganisationCommand.ExternalIdentifier)
        {
            return Results.BadRequest();
        }

        var response = await mediator.Send(updateOrganisationCommand);
        return Results.Ok(response);
    }

    /// <summary>
    /// update organistaion
    /// </summary>
    /// <param name="externalIdentifier"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{externalIdentifier}")]
    public async Task<IResult> DeleteOrganisaionAsync(string externalIdentifier)
    {
        var response = await mediator.Send(new DeleteOrganisationCommand(externalIdentifier));
        return Results.NoContent();
    }
}
