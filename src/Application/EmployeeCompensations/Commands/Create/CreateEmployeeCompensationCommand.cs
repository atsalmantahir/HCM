using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.EmployeeProfiles.Commands.Update;
using HumanResourceManagement.Domain.Enums;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;
using System.Text.Json.Serialization;

namespace HumanResourceManagement.Application.EmployeeCompensations.Commands.Create;

public record CreateEmployeeCompensationCommand : IRequest<CreateEmployeeCompensationCommand>
{
    public EntityExternalIdentifier EmployeeProfile { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal HouseRentAllowance { get; set; }
    public decimal MedicalAllowance { get; set; }
    public decimal UtilityAllowance { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PaymentMethod ModeOfPayment { get; set; }
}

public class CreateEmployeeCompensationCommandHandler : IRequestHandler<CreateEmployeeCompensationCommand, CreateEmployeeCompensationCommand>
{
    private readonly IEmployeeCompensationsRepository repository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;

    public CreateEmployeeCompensationCommandHandler(
        IEmployeeCompensationsRepository repository,
        IEmployeeProfilesRepository employeeProfilesRepository)
    {
        this.repository = repository;
        this.employeeProfilesRepository = employeeProfilesRepository;
    }

    public async Task<CreateEmployeeCompensationCommand> Handle(CreateEmployeeCompensationCommand request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeProfilesRepository
            .GetAsync(request.EmployeeProfile.ExternalIdentifier);

        if (employeeProfile is null)
        {
            throw new EmployeeNotFoundException(request.EmployeeProfile?.ExternalIdentifier);
        }

        var employeeCompensations = this.repository.GetAll();

        if (employeeCompensations.Any(x => x.EmployeeProfileId == employeeProfile.EmployeeProfileId)) 
        {
            return null;
        }

        var entity = request.ToEntity(employeeProfile.EmployeeProfileId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.InsertAsync(entity, cancellationToken);

        return request;
    }
}

public static class CreateEmployeeCompensationCommandExtention
{
    public static CreateEmployeeCompensationCommand StructureRequest(
        this CreateEmployeeCompensationCommand request,
        string employeeProfileExternalIdentifier)
    {
        return new CreateEmployeeCompensationCommand
        {
            BasicSalary = request.BasicSalary,
            EmployeeProfile = new EntityExternalIdentifier 
            {
                ExternalIdentifier = employeeProfileExternalIdentifier
            },
            HouseRentAllowance = request.HouseRentAllowance,
            MedicalAllowance = request.MedicalAllowance,
            ModeOfPayment = request.ModeOfPayment,
            UtilityAllowance = request.UtilityAllowance
        };
    }
}