using HumanResourceManagement.Application.Common.Helpers;
using HumanResourceManagement.Application.EmployeeAttendences.Commands.Create;
using HumanResourceManagement.Application.EmployeeAttendences.Commands.Delete;
using HumanResourceManagement.Application.EmployeeAttendences.Queries.Get;
using HumanResourceManagement.Application.EmployeeAttendences.Queries.List;
using HumanResourceManagement.Application.EmployeeExperiences.Commands.Update;
using HumanResourceManagement.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Api.Controllers;

/// <summary>
/// controller
/// </summary>
[Route("api/employees/profile/")]
[ApiController]
public class EmployeeAttendencesController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="mediator"></param>
    public EmployeeAttendencesController(IMediator mediator)
    {

        this.mediator = mediator;

    }

    /// <summary>
    /// create employee attendence
    /// </summary>
    /// <param name="employeeExternalIdentifier"></param>
    /// <param name="createEmployeeAttendenceCommand"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost]
    [Route("{employeeExternalIdentifier}/attendance")]
    public async Task<CreateEmployeeAttendenceCommand> CreateEmployeeAttendanceAsync(
        string employeeExternalIdentifier,
        [FromBody] CreateEmployeeAttendenceCommand createEmployeeAttendenceCommand)
    {
        var response = await mediator.Send(createEmployeeAttendenceCommand);
        return response;
    }

    [Authorize]
    [HttpPost]
    [Route("attendance/import")]
    public async Task<IResult> ImportEmployeeAttendancesAsync(
        [FromForm] IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return Results.BadRequest("No file uploaded");
            }

            // Read the uploaded file stream
            using (var stream = file.OpenReadStream())
            {
                // Determine the file extension (Excel or CSV)
                string fileExtension = Path.GetExtension(file.FileName)?.ToLower();

                if (fileExtension == ".xlsx" || fileExtension == ".xls")
                {
                    var createEmployeeAttendanceCommands = ExcelHelper.ReadExcelToList(stream, ExcelHelper.MapRowToEmployeeAttendence);
                    // Process the list of people as needed
                    foreach (var command in createEmployeeAttendanceCommands)
                    {
                        var response = await mediator.Send(command);
                    }

                    return Results.Created(string.Empty, "Records uploaded successfully");
                }
                //else if (fileExtension == ".csv")
                //{
                //    List<CreateEmployeeProfileCommand> people = CsvHelper.ReadCsvToList(stream, MapCsvRowToPerson);
                //    // Process the list of people as needed
                //    return Results.Ok(people);
                //}
                else
                {
                    return Results.BadRequest("Unsupported file format. Please upload an Excel (.xlsx, .xls) or CSV (.csv) file.");
                }
            }
        }
        catch (Exception ex)
        {
            return Results.Problem(detail: ex.Message, instance: ex.Message, statusCode: 500);
        }
    }

    [Authorize]
    [HttpPut]
    [Route("{employeeExternalIdentifier}/attendance/{externalIdentifier}")]
    public async Task<IResult> UpdateEmployeeAttendanceAsync(
        string employeeExternalIdentifier,
        string externalIdentifier,
        [FromBody] UpdateEmployeeExperienceCommand request)
    {
        if (employeeExternalIdentifier != request?.EmployeeProfile?.ExternalIdentifier)
        {
            throw new BadRequestException("Employee External Identifier not match");
        }

        if (externalIdentifier != request?.ExternalIdentifier)
        {
            throw new BadRequestException("External Identifier not match");
        }

        var response = await mediator.Send(request.StructureRequest(employeeExternalIdentifier, externalIdentifier));
        return Results.Ok(response);
    }

    [Authorize]
    [HttpDelete]
    [Route("{employeeExternalIdentifier}/attendance/{externalIdentifier}")]
    public async Task<IResult> DeleteEmployeeAttendanceAsync(
        string employeeExternalIdentifier,
        string externalIdentifier)
    {
        var response = await mediator.Send(new DeleteEmployeeAttendenceCommand(employeeExternalIdentifier, externalIdentifier));
        return Results.Ok(response);
    }

    [Authorize]
    [HttpGet]
    [Route("{employeeExternalIdentifier}/attendance")]
    public async Task<IResult> ListEmployeeAttendanceAsync(
    string employeeExternalIdentifier,
        [FromQuery] GetEmployeeAttendencesQuery query)
    {
        var response = await mediator.Send(query);
        return Results.Ok(response);
    }

    [Authorize]
    [HttpGet]
    [Route("{employeeExternalIdentifier}/attendance/{externalIdentifier}")]
    public async Task<IResult> GetEmployeeAttendanceAsync(
        string employeeExternalIdentifier,
        string externalIdentifier)
    {
        var response = await mediator.Send(new GetEmployeeAttendenceQuery(employeeExternalIdentifier, externalIdentifier));
        return Results.Ok(response);
    }
}
