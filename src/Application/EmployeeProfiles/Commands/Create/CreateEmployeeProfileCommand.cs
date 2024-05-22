using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Domain.Constants;
using HumanResourceManagement.Domain.Enums;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HumanResourceManagement.Application.EmployeeProfiles.Commands.Create;

public record CreateEmployeeProfileCommand : IRequest<CreateEmployeeProfileCommand>
{
    public EntityExternalIdentifier Designation { get; set; }

    [Required]
    public string EmployeeName { get; set; }

    [Required]
    public string EmployeeCode { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EmployeeType EmployeeType { get; set; }

    [Required]
    public string Contact { get; set; }

    [Required]
    public string EmailAddress { get; set; }
    public string LineManager { get; set; }
    public string Segment { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender Gender { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MaritalStatus MaritalStatus { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EmployeeStatus ActiveStatus { get; set; }
    public DateTime LastWorkingDayDate { get; set; }
    public DateTime? JoiningDate { get; set; }

}

public class CreateEmployeeProfileCommandHandler : IRequestHandler<CreateEmployeeProfileCommand, CreateEmployeeProfileCommand>
{
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;
    private readonly IDesignationsRepository designationsRepository;
    private readonly IDepartmentsRepository departmentsRepository;
    private readonly IOrganisationsRepository organisationsRepository;
    private readonly IIdentityService identityService;

    public CreateEmployeeProfileCommandHandler(
        IEmployeeProfilesRepository employeeProfilesRepository,
        IDesignationsRepository designationsRepository,
        IDepartmentsRepository departmentsRepository,
        IOrganisationsRepository organisationsRepository,
        IIdentityService identityService)
    {
        this.employeeProfilesRepository = employeeProfilesRepository;
        this.designationsRepository = designationsRepository;
        this.departmentsRepository = departmentsRepository;
        this.organisationsRepository = organisationsRepository;
        this.identityService = identityService;
    }

    public async Task<CreateEmployeeProfileCommand> Handle(CreateEmployeeProfileCommand request, CancellationToken cancellationToken)
    {
        string defaultPassword = "Password1!";
        
        var designation = await this.designationsRepository.GetAsync(request.Designation.ExternalIdentifier);
        if (designation is null)
        {
            throw new DesignationNotFoundException(request.Designation.ExternalIdentifier);
        }

        // Create Identity User with employee profile and set password.
        var registerEmployeeUser = await this
            .identityService
            .CreateUserAsync(request.EmailAddress, request.EmailAddress, defaultPassword, Roles.Employee);

        if (registerEmployeeUser.Result?.Errors?.Count > 0) 
        {
            throw new BadRequestException(registerEmployeeUser.Result?.Errors?.FirstOrDefault());
        }

        var entity = request.ToEmployeeProfileEntity(designation.DesignationId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await employeeProfilesRepository.InsertAsync(entity, cancellationToken);

        return request;
    }
}
