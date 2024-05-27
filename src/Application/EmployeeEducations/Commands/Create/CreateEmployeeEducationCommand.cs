using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeEducations.Commands.Create;

public record CreateEmployeeEducationCommand : IRequest<Result<CreateEmployeeEducationCommand>>
{
    public EmployeeProfileIdentifier EmployeeProfile { get; set; }
    public string Degree { get; set; }
    public string Institution { get; set; }
    public int CompletionYear { get; set; }
}
public class CreateEmployeeEducationCommandHandler : IRequestHandler<CreateEmployeeEducationCommand, Result<CreateEmployeeEducationCommand>>
{
    private readonly IEmployeeEducationsRepository repository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;

    public CreateEmployeeEducationCommandHandler(
        IEmployeeEducationsRepository repository,
        IEmployeeProfilesRepository employeeProfilesRepository)
    {
        this.repository = repository;
        this.employeeProfilesRepository = employeeProfilesRepository;
    }

    public async Task<Result<CreateEmployeeEducationCommand>> Handle(CreateEmployeeEducationCommand request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeProfilesRepository
            .GetAsync(request.EmployeeProfile.Id);

        if (employeeProfile is null) 
        {
            return new Result<CreateEmployeeEducationCommand>(
                succeeded: false, 
                data: null,
                errors: new List<string> 
                {
                    "No Employee Profile Found",
                });
        }

        var entity = request.ToEmployeeEducationEntity(employeeProfile.EmployeeProfileId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.InsertAsync(entity, cancellationToken);

        return new Result<CreateEmployeeEducationCommand>(
                succeeded: true,
                data: request,
                errors: new List<string>());
    }
}

public static class CreateEmployeeEducationCommandExtension 
{
    public static CreateEmployeeEducationCommand StructureRequest(
        this CreateEmployeeEducationCommand request, 
        int id)
    {
        return new CreateEmployeeEducationCommand
        {
            EmployeeProfile = new EmployeeProfileIdentifier
            {
                Id = id,
            },
            Degree = request.Degree,
            CompletionYear = request.CompletionYear,
            Institution = request.Institution
        };
    }
}
