using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.EmployeeProfiles.Commands.Update;
using HumanResourceManagement.Domain.Enums;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;
using System.Text.Json.Serialization;

namespace HumanResourceManagement.Application.EmployeeCompensations.Commands.Create;

public record UpdateEmployeeCompensationCommand : IRequest<UpdateEmployeeCompensationCommand>
{
    public int EmployeeCompenstaionId { get; set; }
    public EntityIdentifier EmployeeProfile { get; set; }
    public decimal BasicSalary { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PaymentMethod ModeOfPayment { get; set; }
}

public class UpdateEmployeeCompensationCommandHandler : IRequestHandler<UpdateEmployeeCompensationCommand, UpdateEmployeeCompensationCommand>
{
    private readonly IEmployeeCompensationsRepository repository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;

    public UpdateEmployeeCompensationCommandHandler(
        IEmployeeCompensationsRepository repository,
        IEmployeeProfilesRepository employeeProfilesRepository)
    {
        this.repository = repository;
        this.employeeProfilesRepository = employeeProfilesRepository;
    }

    public async Task<UpdateEmployeeCompensationCommand> Handle(UpdateEmployeeCompensationCommand request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeProfilesRepository
            .GetAsync(request.EmployeeProfile.Id);

        if (employeeProfile is null)
        {
            throw new EmployeeNotFoundException(request.EmployeeProfile.Id.ToString());
        }

        var employeeCompensation = await this.repository
            .GetAsync(request.EmployeeCompenstaionId);

        if (employeeCompensation is null)
        {
            throw new EmployeeCompensationNotFoundException(request.EmployeeCompenstaionId.ToString());
        }

        if (employeeProfile.EmployeeProfileId != employeeCompensation.EmployeeProfileId) 
        {
            throw new BadRequestException("Employee compensation doesnot belongs to given empolyee identifier");
        }

        var entity = request.ToEntity(employeeCompensation.EmployeeCompensationId, employeeProfile.EmployeeProfileId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.UpdateAsync(entity, cancellationToken);

        return request;
    }
}

public static class UpdateEmployeeCompensationCommandExtention
{
    public static UpdateEmployeeCompensationCommand StructureRequest(
        this UpdateEmployeeCompensationCommand request)
    {
        return new UpdateEmployeeCompensationCommand
        {
            EmployeeCompenstaionId = request.EmployeeCompenstaionId,
            BasicSalary = request.BasicSalary,
            EmployeeProfile = new EntityIdentifier
            {
                Id = request.EmployeeProfile.Id,
            },
            ModeOfPayment = request.ModeOfPayment,
        };
    }
}