using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.EmployeeEducations.Commands.Create;
using HumanResourceManagement.Application.EmployeeExperiences.Commands.Create;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeExperiences.Commands.Update;

public record UpdateEmployeeExperienceCommand : IRequest<UpdateEmployeeExperienceCommand>
{
    public string ExternalIdentifier { get; set; }
    public EmployeeProfileExternalIdentifier EmployeeProfile { get; set; }
    public string CompanyName { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class UpdateEmployeeExperienceCommandHandler : IRequestHandler<UpdateEmployeeExperienceCommand, UpdateEmployeeExperienceCommand>
{
    private readonly IEmployeeExperiencesRepository repository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;


    public UpdateEmployeeExperienceCommandHandler(
        IEmployeeExperiencesRepository repository,
        IEmployeeProfilesRepository employeeProfilesRepository)
    {
        this.repository = repository;
        this.employeeProfilesRepository = employeeProfilesRepository;
    }

    public async Task<UpdateEmployeeExperienceCommand> Handle(UpdateEmployeeExperienceCommand request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeProfilesRepository
            .GetAsync(request.EmployeeProfile.ExternalIdentifier);

        if (employeeProfile is null) 
        {
            throw new EmployeeNotFoundException(request.EmployeeProfile?.ExternalIdentifier);
        }

        var employeeExperience = await this.repository.GetAsync(request.ExternalIdentifier);
        if (employeeExperience is null) 
        {
            throw new EmployeeExperienceNotFoundException(request.ExternalIdentifier);
        }

        var entity = request.ToEmployeeExperienceEntity(employeeProfile.EmployeeProfileId, employeeExperience.EmployeeExperienceId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.UpdateAsync(entity, cancellationToken);

        return request;
    }
}

public static class UpdateEmployeeExperienceCommandExtention
{
    public static UpdateEmployeeExperienceCommand StructureRequest(
        this UpdateEmployeeExperienceCommand request, 
        string employeeExternalIdentifier,
        string externalIdentifier)
    {
        return new UpdateEmployeeExperienceCommand
        {
            ExternalIdentifier = externalIdentifier,
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
