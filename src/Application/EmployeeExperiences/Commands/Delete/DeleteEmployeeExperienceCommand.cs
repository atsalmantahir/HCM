using HumanResourceManagement.Application.EmployeeEducations.Commands.Delete;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeExperiences.Commands.Delete;

public record DeleteEmployeeExperienceCommand(int employeeProfileId, int id) : IRequest<DeleteEmployeeExperienceCommand>
{
    public bool IsDeleted { get; set; }
}

public class DeleteEmployeeExperienceCommandHandler : IRequestHandler<DeleteEmployeeExperienceCommand, DeleteEmployeeExperienceCommand>
{
    private readonly IEmployeeExperiencesRepository repository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;

    public DeleteEmployeeExperienceCommandHandler(IEmployeeExperiencesRepository repository, IEmployeeProfilesRepository employeeProfilesRepository)
    {
        this.repository = repository;
        this.employeeProfilesRepository = employeeProfilesRepository;
    }

    public async Task<DeleteEmployeeExperienceCommand> Handle(DeleteEmployeeExperienceCommand request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeProfilesRepository.GetAsync(request.employeeProfileId);
        if (employeeProfile == null)
        {
            throw new EmployeeNotFoundException(request.employeeProfileId.ToString());
        }

        var employeeExperience = await this.repository.GetAsync(request.id);
        if (employeeExperience == null)
        {
            throw new EmployeeExperienceNotFoundException(request.id.ToString());
        }

        await this.repository.DeleteAsync(employeeExperience, new CancellationToken());

        return new DeleteEmployeeExperienceCommand(request.employeeProfileId, request.id)
        {
            IsDeleted = true,
        };
    }
}
