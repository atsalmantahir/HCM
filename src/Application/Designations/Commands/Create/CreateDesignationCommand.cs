using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.Departments.Commands;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Application.Designations.Commands.Create;

public record CreateDesignationCommand : IRequest<CreateDesignationCommand>
{
    public EntityIdentifier Department { get; set; }

    [Required]
    public string DesignationName { get; set; }
}


public class CreateDesignationCommandHandler : IRequestHandler<CreateDesignationCommand, CreateDesignationCommand>
{
    private readonly IDesignationsRepository repository;
    private readonly IDepartmentsRepository departmentsRepository;

    public CreateDesignationCommandHandler(IDesignationsRepository repository, IDepartmentsRepository departmentsRepository)
    {
        this.repository = repository;
        this.departmentsRepository = departmentsRepository;
    }

    public async Task<CreateDesignationCommand> Handle(CreateDesignationCommand request, CancellationToken cancellationToken)
    {
        var department = await this.departmentsRepository.GetAsync(request.Department.Id);
        if (department is null)
        {
            throw new DepartmentNotFoundException($"Department : '{request.Department.Id}' not found");
        }

        var designations = this.repository.GetAll(request.Department.Id);
        if (designations.Any(x => x.DesignationName == request.DesignationName))
        {
            throw new ConflictRequestException($"Designation : '{request.DesignationName}' already exists");
        }

        var entity = request.ToEntity(department.DepartmentId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.InsertAsync(entity, cancellationToken);

        return request;
    }
}

