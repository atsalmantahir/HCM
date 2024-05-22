using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.EmployeeEducations.Commands.Create;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeExperiences.Commands.Create;

public record CreateEmployeeExperienceCommand : IRequest<CreateEmployeeExperienceCommand>
{
    public EmployeeProfileExternalIdentifier EmployeeProfile { get; set; }
    public string CompanyName { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class CreateEmployeeExperienceCommandHandler : IRequestHandler<CreateEmployeeExperienceCommand, CreateEmployeeExperienceCommand>
{
    private readonly IEmployeeExperiencesRepository repository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;


    public CreateEmployeeExperienceCommandHandler(
        IEmployeeExperiencesRepository repository,
        IEmployeeProfilesRepository employeeProfilesRepository)
    {
        this.repository = repository;
        this.employeeProfilesRepository = employeeProfilesRepository;
    }

    public async Task<CreateEmployeeExperienceCommand> Handle(CreateEmployeeExperienceCommand request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeProfilesRepository
            .GetAsync(request.EmployeeProfile.ExternalIdentifier);

        var entity = request.ToEmployeeExperienceEntity(employeeProfile.EmployeeProfileId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.InsertAsync(entity, cancellationToken);

        return request;
    }
}

public static class CreateEmployeeExperienceCommandExtention
{
    public static CreateEmployeeExperienceCommand StructureRequest(this CreateEmployeeExperienceCommand request, string employeeExternalIdentifier)
    {
        return new CreateEmployeeExperienceCommand
        {
            EmployeeProfile = new EmployeeProfileExternalIdentifier
            {
                ExternalIdentifier = employeeExternalIdentifier,
            },
            CompanyName = request.CompanyName,
            EndDate = request.EndDate,
            Position = request.Position,
            StartDate = request.StartDate
        };
    }
}
