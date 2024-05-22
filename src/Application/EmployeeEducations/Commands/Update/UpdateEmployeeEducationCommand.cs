using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeEducations.Commands.Update;

public record UpdateEmployeeEducationCommand : IRequest<UpdateEmployeeEducationCommand>
{
    public string ExternalIdentifier { get; set; }
    public EmployeeProfileExternalIdentifier EmployeeProfile { get; set; }
    public string Degree { get; set; }
    public string Institution { get; set; }
    public int CompletionYear { get; set; }
}

public class UpdateTaxSlabCommandHandler : IRequestHandler<UpdateEmployeeEducationCommand, UpdateEmployeeEducationCommand>
{
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;
    private readonly IEmployeeEducationsRepository employeeEducationsRepository;

    public UpdateTaxSlabCommandHandler(
        IEmployeeProfilesRepository employeeProfilesRepository,
        IEmployeeEducationsRepository employeeEducationsRepository)
    {
        this.employeeProfilesRepository = employeeProfilesRepository;
        this.employeeEducationsRepository = employeeEducationsRepository;
    }

    public async Task<UpdateEmployeeEducationCommand> Handle(UpdateEmployeeEducationCommand request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeProfilesRepository.GetAsync(request.EmployeeProfile?.ExternalIdentifier);

        if (employeeProfile is null)
        {
            throw new EmployeeNotFoundException(request.EmployeeProfile?.ExternalIdentifier);
        }

        var employeeEducation = await this.employeeEducationsRepository.GetAsync(request.ExternalIdentifier);

        if (employeeEducation is null)
        {
            throw new EmployeeEducationNotFoundException(request.ExternalIdentifier);
        }

        var entity = request.ToEntity(employeeProfile.EmployeeProfileId, employeeEducation.EmployeeEducationId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await employeeEducationsRepository.UpdateAsync(entity, cancellationToken);

        return request;
    }
}

public static class UpdateEmployeeEducationCommandExtention
{
    public static UpdateEmployeeEducationCommand StructureRequest(
        this UpdateEmployeeEducationCommand request,
        string externalIdentifier)
    {
        return new UpdateEmployeeEducationCommand
        {
            ExternalIdentifier = externalIdentifier,
            EmployeeProfile = new EmployeeProfileExternalIdentifier 
            {
                ExternalIdentifier = request.ExternalIdentifier,
            },
            CompletionYear = request.CompletionYear,
            Degree = request.Degree,
            Institution = request.Institution
        };
    }
}