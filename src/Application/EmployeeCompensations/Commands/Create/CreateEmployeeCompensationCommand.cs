using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.EmployeeAllowances.Commands.Create;
using HumanResourceManagement.Application.EmployeeProfiles.Commands.Update;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Enums;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace HumanResourceManagement.Application.EmployeeCompensations.Commands.Create;

public record CreateEmployeeCompensationCommand : IRequest<CreateEmployeeCompensationCommand>
{
    public EntityExternalIdentifier EmployeeProfile { get; set; }
    public decimal BasicSalary { get; set; }

    public List<CreateEmployeeAllowanceCommand> EmployeeAllowances { get; init; }

    // todo: this will be deleted
    public decimal HouseRentAllowance { get; set; }

    // todo: this will be deleted

    public decimal MedicalAllowance { get; set; }

    // todo: this will be deleted

    public decimal UtilityAllowance { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PaymentMethod ModeOfPayment { get; set; }
}

public class CreateEmployeeCompensationCommandHandler : IRequestHandler<CreateEmployeeCompensationCommand, CreateEmployeeCompensationCommand>
{
    private readonly IEmployeeCompensationsRepository repository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;
    private readonly IAllowancesRepository allowancesRepository;

    public CreateEmployeeCompensationCommandHandler(
        IEmployeeCompensationsRepository repository,
        IEmployeeProfilesRepository employeeProfilesRepository,
        IAllowancesRepository allowancesRepository)
    {
        this.repository = repository;
        this.employeeProfilesRepository = employeeProfilesRepository;
        this.allowancesRepository = allowancesRepository;
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

        var allowancesExtIdArray = request.EmployeeAllowances.Select(x => x.Allowance.ExternalIdentifier).ToArray();

        var allowances = this.allowancesRepository
            .GetAll()
            .Where(x => allowancesExtIdArray.Contains(x.ExternalIdentifier));

        if (allowances.Count() != allowancesExtIdArray.Length) 
        {
            return null;
        }

        var employeeAllowances = new List<EmployeeAllowance>();
        foreach (var allowance in allowances)
        {
            var requestAllowance = request.EmployeeAllowances.FirstOrDefault(x => x.Allowance.ExternalIdentifier == allowance.ExternalIdentifier);
            if (requestAllowance != null)
            {
                employeeAllowances.Add(new EmployeeAllowance
                {
                    Allowance = allowance,
                    Amount = requestAllowance.Amount,                    
                });
            }
        }

        var entity = request.ToEntity(employeeProfile.EmployeeProfileId, employeeAllowances);

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
            UtilityAllowance = request.UtilityAllowance,
            EmployeeAllowances = request.EmployeeAllowances,
        };
    }
}