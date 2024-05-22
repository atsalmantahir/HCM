using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.Designations.Commands.Update;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeAttendences.Commands.Update;

public record UpdateEmployeeAttendenceCommand : IRequest<UpdateEmployeeAttendenceCommand>
{
    public string ExternalIdentifier { get; set; }
    public EntityExternalIdentifier EmployeeProfile { get; set; }
    public DateOnly AttendanceDate { get; set; }
    public TimeOnly TimeIn { get; set; }
    public TimeOnly TimeOut { get; set; }
}

public class UpdateEmployeeAttendenceCommandHandler : IRequestHandler<UpdateEmployeeAttendenceCommand, UpdateEmployeeAttendenceCommand>
{
    private readonly IEmployeeAttendencesRepository repository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;

    public UpdateEmployeeAttendenceCommandHandler(
        IEmployeeAttendencesRepository repository,
        IEmployeeProfilesRepository employeeProfilesRepository)
    {
        this.repository = repository;
        this.employeeProfilesRepository = employeeProfilesRepository;   
    }

    public async Task<UpdateEmployeeAttendenceCommand> Handle(UpdateEmployeeAttendenceCommand request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeProfilesRepository.GetAsync(request?.EmployeeProfile?.ExternalIdentifier);
        if (employeeProfile is null)
        {
            throw new EmployeeNotFoundException(request?.EmployeeProfile?.ExternalIdentifier);
        }

        var employeeAttendance = await this.repository.GetAsync(request.ExternalIdentifier);
        if (employeeAttendance is null)
        {
            throw new EmployeeAttendenceNotFoundException(request?.ExternalIdentifier);
        }

        var entity = request.ToEntity(employeeProfile.EmployeeProfileId, employeeAttendance.EmployeeAttendanceId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.UpdateAsync(entity, cancellationToken);

        return request;
    }
}