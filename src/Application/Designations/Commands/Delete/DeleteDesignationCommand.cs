using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Designations.Commands.Delete;

public record DeleteDesignationCommand(int departmentId, int Id) : IRequest<DeleteDesignationCommand>
{
    public EntityIdentifier Department { get; set; }
    public bool IsDeleted { get; set; }
}

public class DeleteDesignationCommandHandler : IRequestHandler<DeleteDesignationCommand, DeleteDesignationCommand>
{
    private readonly IDesignationsRepository repository;

    public DeleteDesignationCommandHandler(IDesignationsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<DeleteDesignationCommand> Handle(DeleteDesignationCommand request, CancellationToken cancellationToken)
    {
        var designation = await this.repository.GetAsync(request.Id);
        if (designation == null)
        {
            throw new DesignationNotFoundException(request.Id.ToString());
        }

        await this.repository.DeleteAsync(designation, new CancellationToken());

        return new DeleteDesignationCommand(request.departmentId, request.Id)
        {
            IsDeleted = true,
        };
    }
}
