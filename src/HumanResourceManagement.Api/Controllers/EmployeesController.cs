using HumanResourceManagement.Application.Common.Helpers;
using HumanResourceManagement.Application.EmployeeCompensations.Commands.Create;
using HumanResourceManagement.Application.EmployeeEducations.Commands.Create;
using HumanResourceManagement.Application.EmployeeExperiences.Commands.Create;
using HumanResourceManagement.Application.EmployeeExperiences.Commands.Update;
using HumanResourceManagement.Application.EmployeeProfiles.Commands.Create;
using HumanResourceManagement.Application.EmployeeProfiles.Commands.Delete;
using HumanResourceManagement.Application.EmployeeProfiles.Commands.Update;
using HumanResourceManagement.Application.EmployeeProfiles.Queries.Get;
using HumanResourceManagement.Application.EmployeeProfiles.Queries.List;
using HumanResourceManagement.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Api.Controllers;

/// <summary>
/// employee controller
/// </summary>

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="mediator"></param>
    public EmployeesController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// create employee
    /// </summary>
    /// <param name="createEmployeeProfileCommand"></param>
    /// <returns></returns>
    [Authorize]

    [HttpPost]
    [Route("profile")]
    public async Task<IResult> CreateEmployeeProfileAsync(
        [FromBody] CreateEmployeeProfileCommand createEmployeeProfileCommand)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        var response = await mediator.Send(createEmployeeProfileCommand);
        return Results.Created(string.Empty, response);
    }

    /// <summary>
    /// upload employee excel
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("profile/import")]
    public async Task<IResult> UploadEmployeeProfilesFromExcelAsync([FromForm] IFormFile file)
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
                    var createEmployeeProfileCommands = ExcelHelper.ReadExcelToList(stream, ExcelHelper.MapRowToEmployeeProfile);
                    // Process the list of people as needed
                    foreach (var command in createEmployeeProfileCommands)
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

    /// <summary>
    /// update profile
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="BadRequestException"></exception>
    [Authorize]
    [HttpPut]
    [Route("profile/{id}")]
    public async Task<IResult> UpdateEmployeeProfileAsync(
        int id,
        [FromBody] UpdateEmployeeProfileCommand request)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        if (id != request.EmployeeProfileId)
        {
            throw new BadRequestException("Employee External Identifier not match");
        }

        var response = await mediator.Send(request.StructureRequest(id));
        return Results.Ok(response);
    }

    /// <summary>
    /// delete employee profile
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete]
    [Route("profile/{id}")]
    public async Task<IResult> DeleteEmployeeProfileAsync(
        int id)
    {
        var response = await mediator.Send(new DeleteEmployeeProfileCommand(id));
        return Results.Ok(response);
    }

    /// <summary>
    /// list employee profile
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    [Route("profile")]
    public async Task<IResult> ListEmployeeProfileAsync([FromQuery] GetEmployeeProfilesQuery query)
    {
        var response = await mediator.Send(query).ConfigureAwait(false);
        return Results.Ok(response);
    }

    /// <summary>
    /// get employee profile
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    [Route("profile/{id}")]
    public async Task<IResult> GetEmployeeProfileAsync(
        int id)
    {
        var response = await mediator.Send(new GetEmployeeProfileQuery(id));
        return Results.Ok(response);
    }
}
