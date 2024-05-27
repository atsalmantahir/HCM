using HumanResourceManagement.Application.EmployeeExperiences.Commands.Delete;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeProfiles.Commands.Delete;

public record DeleteEmployeeProfileCommand(int id) : IRequest<DeleteEmployeeProfileCommand>
{
    public bool IsDeleted { get; set; }
}

public class DeleteEmployeeProfileCommandHandler : IRequestHandler<DeleteEmployeeProfileCommand, DeleteEmployeeProfileCommand>
{
    private readonly IEmployeeProfilesRepository repository;

    public DeleteEmployeeProfileCommandHandler(IEmployeeProfilesRepository repository)
    {
        this.repository = repository;
    }

    public async Task<DeleteEmployeeProfileCommand> Handle(DeleteEmployeeProfileCommand request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.repository.GetAsync(request.id);
        if (employeeProfile == null)
        {
            throw new EmployeeNotFoundException(request.id.ToString());
        }

        await this.repository.DeleteAsync(employeeProfile, new CancellationToken());

        return new DeleteEmployeeProfileCommand(request.id)
        {
            IsDeleted = true,
        };
    }
}