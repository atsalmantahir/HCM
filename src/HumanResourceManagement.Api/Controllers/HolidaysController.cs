using HumanResourceManagement.Application.Holidays.Commands.Create;
using HumanResourceManagement.Application.Holidays.Commands.Delete;
using HumanResourceManagement.Application.Holidays.Commands.Update;
using HumanResourceManagement.Application.Holidays.Queries.Get;
using HumanResourceManagement.Application.Holidays.Queries.List;
using HumanResourceManagement.Application.TaxSlabs.Commands.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Api.Controllers;

/// <summary>
/// controller
/// </summary>

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class HolidaysController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="mediator"></param>
    public HolidaysController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// create
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResult> CreateHolidayAsync(
        [FromBody] CreateHolidayCommand request)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        var response = await mediator.Send(request);
        return Results.Ok(response);
    }

    /// <summary>
    /// update
    /// </summary>
    /// <param name="externalIdentifier"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{externalIdentifier}")]
    public async Task<IResult> UpdateHolidayAsync(
        string externalIdentifier,
        [FromBody] UpdateHolidayCommand request)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        if (externalIdentifier != request?.ExternalIdentifier)
        {
            return Results.BadRequest("External Identifier not match");
        }

        var response = await mediator.Send(request.StructureRequest(externalIdentifier));
        return Results.Ok(response);
    }

    /// <summary>
    /// delete
    /// </summary>
    /// <param name="externalIdentifier"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{externalIdentifier}")]
    public async Task<IResult> DeleteHolidayAsync(
        string externalIdentifier)
    {
        var response = await mediator.Send(new DeleteHolidayCommand(externalIdentifier));
        return Results.Ok(response);
    }

    /// <summary>
    /// list
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IResult> ListHolidayAsync([FromQuery] GetHolidaysQuery query)
    {
        var response = await mediator.Send(query);
        return Results.Ok(response);
    }

    /// <summary>
    /// get single
    /// </summary>
    /// <param name="externalIdentifier"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{externalIdentifier}")]
    public async Task<IResult> GetHolidayAsync(
        string externalIdentifier)
    {
        var response = await mediator.Send(new GetHolidayQuery(externalIdentifier));
        return Results.Ok(response);
    }
}
