using HumanResourceManagement.Application.EmployeeProfiles.Commands.Delete;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Organisations.Commands.Delete;

public record DeleteOrganisationCommand(int id) : IRequest<DeleteOrganisationCommand>
{
    public bool IsDeleted { get; set; }
}

public class DeleteOrganisationCommandHandler : IRequestHandler<DeleteOrganisationCommand, DeleteOrganisationCommand>
{
    private readonly IOrganisationsRepository repository;

    public DeleteOrganisationCommandHandler(IOrganisationsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<DeleteOrganisationCommand> Handle(DeleteOrganisationCommand request, CancellationToken cancellationToken)
    {
        var organisation = await this.repository.GetAsync(request.id);
        if (organisation == null)
        {
            return null;
        }

        await this.repository.DeleteAsync(organisation, new CancellationToken());

        return new DeleteOrganisationCommand(request.id)
        {
            IsDeleted = true,
        };
    }
}
