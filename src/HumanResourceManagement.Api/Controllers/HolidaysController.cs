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
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    public async Task<IResult> UpdateHolidayAsync(
        int id,
        [FromBody] UpdateHolidayCommand request)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        if (id != request?.id)
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
    public async Task<IResult> DeleteHolidayAsync(
        int id)
    {
        var response = await mediator.Send(new DeleteHolidayCommand(id));
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
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<IResult> GetHolidayAsync(
        int id)
    {
        var response = await mediator.Send(new GetHolidayQuery(id));
        return Results.Ok(response);
    }
}
