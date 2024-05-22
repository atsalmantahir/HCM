using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.Designations.Commands;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeAttendences.Commands.Create;

public record CreateEmployeeAttendenceCommand : IRequest<CreateEmployeeAttendenceCommand>
{
    public EntityExternalIdentifier EmployeeProfile { get; set; }
    public string AttendanceDate { get; set; }
    public string TimeIn { get; set; }
    public string TimeOut { get; set; }
}

public class CreateEmployeeAttendenceCommandHandler : IRequestHandler<CreateEmployeeAttendenceCommand, CreateEmployeeAttendenceCommand>
{
    private readonly IEmployeeAttendencesRepository repository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;

    public CreateEmployeeAttendenceCommandHandler(
        IEmployeeAttendencesRepository repository, 
        IEmployeeProfilesRepository employeeProfilesRepository)
    {
        this.repository = repository;
        this.employeeProfilesRepository = employeeProfilesRepository;
    }

    public async Task<CreateEmployeeAttendenceCommand> Handle(CreateEmployeeAttendenceCommand request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeProfilesRepository.GetAsync(request?.EmployeeProfile?.ExternalIdentifier);
        if (employeeProfile is null)
        {
            throw new EmployeeNotFoundException(request.EmployeeProfile.ExternalIdentifier);
        }

        var employeeAttendance = this.repository.GetAll();
        var attendanceDate = DateOnly.Parse(request.AttendanceDate);
        if (employeeAttendance is not null && employeeAttendance.Any(x => x.AttendanceDate == attendanceDate)) 
        {
            throw new ConflictRequestException("Already added attendance for this date");
        }

        var entity = request.ToEntity(employeeProfile.EmployeeProfileId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.InsertAsync(entity, cancellationToken);

        return request;
    }
}

