using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeAttendences.Commands.Delete;

public record DeleteEmployeeAttendenceCommand(int employeeProfileId, int Id) : IRequest<DeleteEmployeeAttendenceCommand>
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
        var employeeProfile = await this.employeeProfilesRepository.GetAsync(request.employeeProfileId);
        if (employeeProfile == null)
        {
            throw new EmployeeNotFoundException(request.employeeProfileId.ToString());
        }

        var employeeAttendance = await this.repository.GetAsync(request.Id);
        if (employeeAttendance == null)
        {
            throw new EmployeeAttendenceNotFoundException(request.Id.ToString());
        }

        await this.repository.DeleteAsync(employeeAttendance, new CancellationToken());

        return new DeleteEmployeeAttendenceCommand(request.employeeProfileId, request.Id)
        {
            IsDeleted = true,
        };
    }
}
