using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Domain.Enums;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HumanResourceManagement.Application.EmployeeProfiles.Commands.Update;

public record UpdateEmployeeProfileCommand : IRequest<UpdateEmployeeProfileCommand>
{
    public EntityExternalIdentifier Designation { get; set; }

    [Required]
    public string ExternalIdentifier { get; set; }
    
    [Required]
    public string EmployeeName { get; set; }

    [Required]
    public string EmployeeCode { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EmployeeType EmployeeType { get; set; }

    [Required]
    public string Contact { get; set; }
    public string LineManager { get; set; }
    public string Segment { get; set; }
    public Gender Gender { get; set; }
    public MaritalStatus MaritalStatus { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EmployeeStatus ActiveStatus { get; set; }
    public DateTime LastWorkingDayDate { get; set; }
    public DateTime? JoiningDate { get; set; }

}

public class UpdateEmployeeProfileCommandHandler : IRequestHandler<UpdateEmployeeProfileCommand, UpdateEmployeeProfileCommand>
{
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;
    private readonly IDesignationsRepository designationsRepository;
    private readonly IDepartmentsRepository departmentsRepository;
    private readonly IOrganisationsRepository organisationsRepository;


    public UpdateEmployeeProfileCommandHandler(
        IEmployeeProfilesRepository employeeProfilesRepository,
        IDesignationsRepository designationsRepository,
        IDepartmentsRepository departmentsRepository,
        IOrganisationsRepository organisationsRepository
        )
    {
        this.employeeProfilesRepository = employeeProfilesRepository;
        this.designationsRepository = designationsRepository;
        this.departmentsRepository = departmentsRepository;
        this.organisationsRepository = organisationsRepository;
    }

    public async Task<UpdateEmployeeProfileCommand> Handle(UpdateEmployeeProfileCommand request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeProfilesRepository
            .GetAsync(request.ExternalIdentifier);

        if (employeeProfile is null)
        {
            throw new EmployeeNotFoundException(request.ExternalIdentifier);
        }

        var designation = await this.designationsRepository .GetAsync(request.Designation.ExternalIdentifier);
        if (designation is null)
        {
            throw new DesignationNotFoundException(request.Designation?.ExternalIdentifier);
        }

        var entity = request.ToEmployeeProfileEntity(
            employeeProfile.EmployeeProfileId, 
            designation.DesignationId,
            employeeProfile.EmailAddress);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await employeeProfilesRepository.UpdateAsync(entity, cancellationToken);

        return request;
    }
}

public static class UpdateEmployeeProfileCommandExtention
{
    public static UpdateEmployeeProfileCommand StructureRequest(
        this UpdateEmployeeProfileCommand request,
        string externalIdentifier)
    {
        return new UpdateEmployeeProfileCommand
        {
            ExternalIdentifier = externalIdentifier,
            Contact = request.Contact,
            Designation = new EntityExternalIdentifier { ExternalIdentifier = request.Designation.ExternalIdentifier },
            ActiveStatus = request.ActiveStatus,
            LastWorkingDayDate = request.LastWorkingDayDate,
            EmployeeCode = request.EmployeeCode,
            EmployeeName = request.EmployeeName,
            EmployeeType = request.EmployeeType,
            Gender = request.Gender,
            MaritalStatus = request.MaritalStatus,
            LineManager = request.LineManager,
            Segment = request.Segment            
        };
    }
}
