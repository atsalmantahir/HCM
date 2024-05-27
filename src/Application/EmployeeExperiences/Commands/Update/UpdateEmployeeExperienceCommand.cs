using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.EmployeeEducations.Commands.Create;
using HumanResourceManagement.Application.EmployeeExperiences.Commands.Create;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeExperiences.Commands.Update;

public record UpdateEmployeeExperienceCommand : IRequest<UpdateEmployeeExperienceCommand>
{
    public int Id { get; set; }
    public EmployeeProfileIdentifier EmployeeProfile { get; set; }
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
            .GetAsync(request.EmployeeProfile.Id);

        if (employeeProfile is null) 
        {
            throw new EmployeeNotFoundException(request.EmployeeProfile.Id.ToString());
        }

        var employeeExperience = await this.repository.GetAsync(request.Id);
        if (employeeExperience is null) 
        {
            throw new EmployeeExperienceNotFoundException(request.Id.ToString());
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
        int employeeProfileId,
        int id)
    {
        return new UpdateEmployeeExperienceCommand
        {
            Id = id,
            EmployeeProfile = new EmployeeProfileIdentifier
            {
                Id = employeeProfileId,
            },
            CompanyName = request.CompanyName,
            EndDate = request.EndDate,
            Position = request.Position,
            StartDate = request.StartDate
        };
    }
}
