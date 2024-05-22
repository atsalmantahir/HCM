using HumanResourceManagement.Application.EmployeeAttendences.Commands.Delete;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeCompensations.Commands.Delete;

public record DeleteEmployeeCompensationCommand(string employeeProfileExternalIdentifier, string externalIdentifier) : IRequest<DeleteEmployeeCompensationCommand>
{
    public bool IsDeleted { get; set; }
}

public class DeleteEmployeeCompensationCommandHandler : IRequestHandler<DeleteEmployeeCompensationCommand, DeleteEmployeeCompensationCommand>
{
    private readonly IEmployeeCompensationsRepository repository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;

    public DeleteEmployeeCompensationCommandHandler(
        IEmployeeCompensationsRepository repository, 
        IEmployeeProfilesRepository employeeProfilesRepository)
    {
        this.repository = repository;
        this.employeeProfilesRepository = employeeProfilesRepository;
    }

    public async Task<DeleteEmployeeCompensationCommand> Handle(DeleteEmployeeCompensationCommand request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeProfilesRepository.GetAsync(request.employeeProfileExternalIdentifier);
        if (employeeProfile == null)
        {
            throw new EmployeeNotFoundException(request.employeeProfileExternalIdentifier);
        }

        var employeeCompensation = await this.repository.GetAsync(request.externalIdentifier);
        if (employeeCompensation == null)
        {
            throw new EmployeeCompensationNotFoundException(request.externalIdentifier);
        }

        await this.repository.DeleteAsync(employeeCompensation, new CancellationToken());

        return new DeleteEmployeeCompensationCommand(request.employeeProfileExternalIdentifier, request.externalIdentifier)
        {
            IsDeleted = true,
        };
    }
}
