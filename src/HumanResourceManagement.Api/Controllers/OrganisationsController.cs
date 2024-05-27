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
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<OrganisationVM> GetOgranistaionAsync(int id)
    {
        var response = await mediator.Send(new GetOrganisationQuery(id));
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
    /// <param name="id"></param>
    /// <param name="updateOrganisationCommand"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    public async Task<IResult> UpdateOrganisaionAsync(int id,
        [FromBody] UpdateOrganisationCommand updateOrganisationCommand)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        if (id != updateOrganisationCommand.Id)
        {
            return Results.BadRequest();
        }

        var response = await mediator.Send(updateOrganisationCommand);
        return Results.Ok(response);
    }

    /// <summary>
    /// update organistaion
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<IResult> DeleteOrganisaionAsync(int id)
    {
        var response = await mediator.Send(new DeleteOrganisationCommand(id));
        return Results.NoContent();
    }
}
