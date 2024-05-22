using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Application.Designations.Commands.Update;

public class UpdateDesignationCommand : IRequest<UpdateDesignationCommand>
{
    public EntityExternalIdentifier Department { get; set; }

    [Required]
    public string ExternalIdentifier { get; set; }

    [Required]
    public string DesignationName { get; set; }
}

public class UpdateDesignationCommandHandler : IRequestHandler<UpdateDesignationCommand, UpdateDesignationCommand>
{
    private readonly IDesignationsRepository repository;
    private readonly IDepartmentsRepository departmentsRepository;

    public UpdateDesignationCommandHandler(IDesignationsRepository repository, IDepartmentsRepository departmentsRepository)
    {
        this.repository = repository;
        this.departmentsRepository = departmentsRepository;
    }

    public async Task<UpdateDesignationCommand> Handle(UpdateDesignationCommand request, CancellationToken cancellationToken)
    {
        var department = await this.departmentsRepository.GetAsync(request.Department.ExternalIdentifier);
        if (department is null)
        {
            throw new DepartmentNotFoundException($"Department : '{request.Department.ExternalIdentifier}' not found");
        }

        var designation = await this.repository.GetAsync(request.Department.ExternalIdentifier, request.ExternalIdentifier);
        if (designation is null) 
        {
            throw new DesignationNotFoundException(request.ExternalIdentifier);
        }

        var designations = this.repository.GetAll(request.Department.ExternalIdentifier);
        if (designations.Any(x => x.DesignationName == request.DesignationName))
        {
            throw new ConflictRequestException($"Designation : '{request.DesignationName}' already exists");
        }

        var entity = request.ToEntity(department.DepartmentId, designation.DesignationId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.UpdateAsync(entity, cancellationToken);

        return request;
    }
}