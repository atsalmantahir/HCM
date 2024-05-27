using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.Departments.Commands.Create;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Application.Departments.Commands.Update;

public class UpdateDepartmentCommand : IRequest<UpdateDepartmentCommand>
{

    public EntityIdentifier Organisation { get; set; }

    [Required]
    public int Id { get; set; }

    [Required]
    public string DepartmentName { get; set; }
}


public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, UpdateDepartmentCommand>
{
    private readonly IDepartmentsRepository repository;
    private readonly IOrganisationsRepository organisationsRepository;

    public UpdateDepartmentCommandHandler(
        IDepartmentsRepository repository, IOrganisationsRepository organisationsRepository)
    {
        this.repository = repository;
        this.organisationsRepository = organisationsRepository;
    }

    public async Task<UpdateDepartmentCommand> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var organistaion = await this.organisationsRepository.GetAsync(request.Organisation.Id);
        if (organistaion is null)
        {
            throw new OrganisationNotFoundException(request.Organisation.Id.ToString());
        }

        var department = await this.repository.GetAsync(request.Id);
        if (department is null) 
        {
            throw new DepartmentNotFoundException(request.Id.ToString());
        }

        var departments = this.repository.GetAll(organistaion.OrganisationId);
        if (departments.Any(x => x.DepartmentName == request.DepartmentName))
        {
            throw new ConflictRequestException($"Department : '{request.DepartmentName}' already exists");
        }


        var entity = request.ToEntity(organistaion.OrganisationId, department.DepartmentId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.UpdateAsync(entity, cancellationToken);

        return request;
    }
}
