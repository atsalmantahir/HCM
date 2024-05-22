using HumanResourceManagement.Application.EmployeeProfiles.Queries.List;
using HumanResourceManagement.Application.Payrolls.Commands.Create;
using HumanResourceManagement.Application.Payrolls.Queries.List;
using HumanResourceManagement.Application.Payrolls.Services;
using HumanResourceManagement.Application.Payrolls.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Api.Controllers;

/// <summary>
/// payroll controller
/// </summary>

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PayrollsController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IPayrollService payrollService;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="payrollService"></param>
    public PayrollsController(
        IMediator mediator,
        IPayrollService payrollService)
    {
        this.mediator = mediator;
        this.payrollService = payrollService;
    }

    /// <summary>
    /// create payroll
    /// </summary>
    /// <param name="createPayrollCommand"></param>
    /// <returns></returns>
    //[HttpPost]
    //public async Task<CreatePayrollCommand> CreatePayrollAsync(
    //    [FromBody] CreatePayrollCommand createPayrollCommand)
    //{
    //    var response = await mediator.Send(createPayrollCommand);
    //    return response;
    //}

    /// <summary>
    /// list payroll
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    /// 
    [HttpGet(Name = "List Payrolls")]
    public async Task<IResult> ListPayrollsAsync([FromQuery] GetPayrollsQuery query)
    {
        var response = await mediator.Send(query).ConfigureAwait(false);
        return Results.Ok(response);
    }

    /// <summary>
    /// generation of payroll for organisation employees
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("generate")]
    public async Task<IActionResult> GeneratePayrollAsync([FromBody] PayrollRequest request)
    {
        await this.payrollService.GeneratePayrollAsync(request);
        return Ok("Payroll Generated");
    }

    [HttpPost]
    [Route("calculate/taxation")]
    public async Task<IResult> CalculateTaxation() 
    {
        var taxationResult = await this.payrollService.CalculateTaxation();
        return Results.Ok(taxationResult);  
    }
}
