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
    /// <param name="employeeId"></param>
    /// <param name="createEmployeeAttendenceCommand"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost]
    [Route("{employeeId}/attendance")]
    public async Task<CreateEmployeeAttendenceCommand> CreateEmployeeAttendanceAsync(
        int employeeId,
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
    [Route("{employeeId}/attendance/{id}")]
    public async Task<IResult> UpdateEmployeeAttendanceAsync(
        int employeeId,
        int id,
        [FromBody] UpdateEmployeeExperienceCommand request)
    {
        if (employeeId != request?.EmployeeProfile?.Id)
        {
            throw new BadRequestException("Employee External Identifier not match");
        }

        if (id != request?.Id)
        {
            throw new BadRequestException("External Identifier not match");
        }

        var response = await mediator.Send(request.StructureRequest(employeeId, id));
        return Results.Ok(response);
    }

    [Authorize]
    [HttpDelete]
    [Route("{employeeId}/attendance/{id}")]
    public async Task<IResult> DeleteEmployeeAttendanceAsync(
        int employeeId,
        int id)
    {
        var response = await mediator.Send(new DeleteEmployeeAttendenceCommand(employeeId, id));
        return Results.Ok(response);
    }

    [Authorize]
    [HttpGet]
    [Route("{employeeId}/attendance")]
    public async Task<IResult> ListEmployeeAttendanceAsync(
    int employeeId,
        [FromQuery] GetEmployeeAttendencesQuery query)
    {
        var response = await mediator.Send(query);
        return Results.Ok(response);
    }

    [Authorize]
    [HttpGet]
    [Route("{employeeId}/attendance/{id}")]
    public async Task<IResult> GetEmployeeAttendanceAsync(
        int employeeId,
        int id)
    {
        var response = await mediator.Send(new GetEmployeeAttendenceQuery(employeeId, id));
        return Results.Ok(response);
    }
}
