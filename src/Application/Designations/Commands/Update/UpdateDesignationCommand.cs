using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Application.Designations.Commands.Update;

public class UpdateDesignationCommand : IRequest<UpdateDesignationCommand>
{
    public EntityIdentifier Department { get; set; }

    [Required]
    public int Id { get; set; }

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
        var department = await this.departmentsRepository.GetAsync(request.Department.Id);
        if (department is null)
        {
            throw new DepartmentNotFoundException($"Department : '{request.Department.Id}' not found");
        }

        var designation = await this.repository.GetAsync(request.Id);
        if (designation is null) 
        {
            throw new DesignationNotFoundException(request.Id.ToString());
        }

        var designations = this.repository.GetAll(request.Department.Id);
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