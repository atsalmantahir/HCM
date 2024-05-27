using HumanResourceManagement.Application.EmployeeCompensations.Commands.Delete;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeEducations.Commands.Delete;

public record DeleteEmployeeEducationCommand(int employeeProfileId, int Id) : IRequest<DeleteEmployeeEducationCommand>
{
    public bool IsDeleted { get; set; }
}

public class DeleteEmployeeEducationCommandHandler : IRequestHandler<DeleteEmployeeEducationCommand, DeleteEmployeeEducationCommand>
{
    private readonly IEmployeeEducationsRepository repository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;


    public DeleteEmployeeEducationCommandHandler(IEmployeeEducationsRepository repository, IEmployeeProfilesRepository employeeProfilesRepository)
    {
        this.repository = repository;
        this.employeeProfilesRepository = employeeProfilesRepository;
    }

    public async Task<DeleteEmployeeEducationCommand> Handle(DeleteEmployeeEducationCommand request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeProfilesRepository.GetAsync(request.employeeProfileId);
        if (employeeProfile == null)
        {
            return null;
        }

        var employeeEducation = await this.repository.GetAsync(request.Id);
        if (employeeEducation == null)
        {
            return null;
        }

        await this.repository.DeleteAsync(employeeEducation, new CancellationToken());

        return new DeleteEmployeeEducationCommand(request.employeeProfileId, request.Id)
        {
            IsDeleted = true,
        };
    }
}
