using HumanResourceManagement.Application.EmployeeEducations.Commands.Delete;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeExperiences.Commands.Delete;

public record DeleteEmployeeExperienceCommand(string employeeProfileExternalIdentifier, string externalIdentifier) : IRequest<DeleteEmployeeExperienceCommand>
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
        var employeeProfile = await this.employeeProfilesRepository.GetAsync(request.employeeProfileExternalIdentifier);
        if (employeeProfile == null)
        {
            throw new EmployeeNotFoundException(request.employeeProfileExternalIdentifier);
        }

        var employeeExperience = await this.repository.GetAsync(request.externalIdentifier);
        if (employeeExperience == null)
        {
            throw new EmployeeExperienceNotFoundException(request.externalIdentifier);
        }

        await this.repository.DeleteAsync(employeeExperience, new CancellationToken());

        return new DeleteEmployeeExperienceCommand(request.employeeProfileExternalIdentifier, request.externalIdentifier)
        {
            IsDeleted = true,
        };
    }
}
