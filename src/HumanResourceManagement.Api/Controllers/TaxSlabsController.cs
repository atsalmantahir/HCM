using HumanResourceManagement.Application.Departments.Queries.Get;
using HumanResourceManagement.Application.Departments.Queries.List;
using HumanResourceManagement.Application.EmployeeEducations.Commands.Create;
using HumanResourceManagement.Application.EmployeeExperiences.Commands.Update;
using HumanResourceManagement.Application.TaxSlabs.Commands.Create;
using HumanResourceManagement.Application.TaxSlabs.Commands.Delete;
using HumanResourceManagement.Application.TaxSlabs.Commands.Update;
using HumanResourceManagement.Application.TaxSlabs.Queries.Get;
using HumanResourceManagement.Application.TaxSlabs.Queries.List;
using HumanResourceManagement.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Api.Controllers;

/// <summary>
/// Tax slab controller
/// </summary>
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TaxSlabsController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="mediator"></param>
    public TaxSlabsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// create
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResult> CreateTaxSlabAsync(
        [FromBody] CreateTaxSlabCommand request)
    {
        var response = await mediator.Send(request);
        return Results.Ok(response);
    }

    /// <summary>
    /// update
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    public async Task<IResult> UpdateTaxSlabAsync(
        int id,
        [FromBody] UpdateTaxSlabCommand request)
    {
        if (id != request?.Id)
        {
            return Results.BadRequest("External Identifier not match");
        }

        var response = await mediator.Send(request.StructureRequest(id));
        return Results.Ok(response);
    }

    /// <summary>
    /// delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<IResult> DeleteTaxSlabAsync(
        int id)
    {
        var response = await mediator.Send(new DeleteTaxSlabCommand(id));
        return Results.Ok(response);
    }

    /// <summary>
    /// list
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IResult> ListTaxSlabAsync([FromQuery] GetTaxSlabsQuery query)
    {
        var response = await mediator.Send(query);
        return Results.Ok(response);
    }

    /// <summary>
    /// get single
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<IResult> GetTaxSlabAsync(
        int id)
    {
        var response = await mediator.Send(new GetTaxSlabQuery(id));
        return Results.Ok(response);
    }
}
