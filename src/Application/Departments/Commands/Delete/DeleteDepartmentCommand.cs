using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Departments.Commands.Delete;

public record DeleteDepartmentCommand(int organisationId, int id) : IRequest<DeleteDepartmentCommand>
{
    public bool IsDeleted { get; set; }
}

public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, DeleteDepartmentCommand>
{
    private readonly IDepartmentsRepository repository;
    private readonly IOrganisationsRepository organisationsRepository;

    public DeleteDepartmentCommandHandler(IDepartmentsRepository repository, IOrganisationsRepository organisationsRepository)
    {
        this.repository = repository;
        this.organisationsRepository = organisationsRepository;
    }

    public async Task<DeleteDepartmentCommand> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var organistaion = await this.organisationsRepository.GetAsync(request.organisationId);
        if (organistaion is null)
        {
            throw new OrganisationNotFoundException(request.organisationId.ToString());
        }

        var department = await this.repository.GetAsync(request.id);
        if (department == null)
        {
            throw new DepartmentNotFoundException(request.id.ToString());
        }

        await this.repository.DeleteAsync(department, new CancellationToken());

        return new DeleteDepartmentCommand(request.organisationId, request.id)
        {
            IsDeleted = true,
        };
    }
}