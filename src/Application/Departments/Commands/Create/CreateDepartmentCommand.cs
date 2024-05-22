using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Application.Departments.Commands.Create;

public record CreateDepartmentCommand : IRequest<CreateDepartmentCommand>
{
    public EntityExternalIdentifier Organisation { get; set; }

    [Required]
    public string DepartmentName { get; set; }
}

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, CreateDepartmentCommand>
{
    private readonly IDepartmentsRepository repository;
    private readonly IOrganisationsRepository organisationsRepository;

    public CreateDepartmentCommandHandler(
        IDepartmentsRepository repository, 
        IOrganisationsRepository organisationsRepository)
    {
        this.repository = repository;
        this.organisationsRepository = organisationsRepository;
    }

    public async Task<CreateDepartmentCommand> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var organistaion = await this.organisationsRepository.GetAsync(request.Organisation.ExternalIdentifier);
        if (organistaion is null)
        {
            throw new OrganisationNotFoundException(request.Organisation.ExternalIdentifier);
        }

        var departments = this.repository.GetAll(request.Organisation?.ExternalIdentifier);
        if (departments.Any(x => x.DepartmentName == request.DepartmentName)) 
        {
            throw new ConflictRequestException($"Department : '{request.DepartmentName}' already exists");
        }

        var entity = request.ToEntity(organistaion.OrganisationId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.InsertAsync(entity, cancellationToken);

        return request;
    }
}
