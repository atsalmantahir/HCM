using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeAttendences.Commands.Delete;

public record DeleteEmployeeAttendenceCommand(string employeeProfileExternalIdentifier, string externalIdentifier) : IRequest<DeleteEmployeeAttendenceCommand>
{
    public bool IsDeleted { get; set; }
}

public class DeleteEmployeeAttendenceCommandHandler : IRequestHandler<DeleteEmployeeAttendenceCommand, DeleteEmployeeAttendenceCommand>
{
    private readonly IEmployeeAttendencesRepository repository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;


    public DeleteEmployeeAttendenceCommandHandler(
        IEmployeeAttendencesRepository repository,
        IEmployeeProfilesRepository employeeProfilesRepository)
    {
        this.repository = repository;
        this.employeeProfilesRepository = employeeProfilesRepository;
    }

    public async Task<DeleteEmployeeAttendenceCommand> Handle(DeleteEmployeeAttendenceCommand request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeProfilesRepository.GetAsync(request.employeeProfileExternalIdentifier);
        if (employeeProfile == null)
        {
            throw new EmployeeNotFoundException(request.employeeProfileExternalIdentifier);
        }

        var employeeAttendance = await this.repository.GetAsync(request.externalIdentifier);
        if (employeeAttendance == null)
        {
            throw new EmployeeAttendenceNotFoundException(request.externalIdentifier);
        }

        await this.repository.DeleteAsync(employeeAttendance, new CancellationToken());

        return new DeleteEmployeeAttendenceCommand(request.employeeProfileExternalIdentifier, request.externalIdentifier)
        {
            IsDeleted = true,
        };
    }
}
