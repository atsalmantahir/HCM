using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Departments.Commands.Create;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Application.Allowances.Commands.Create;

public record CreateAllowanceCommand : IRequest<CreateAllowanceCommand>
{
    [Required]
    public string Name { get; set; }
}

public class CreateAllowanceCommandHandler : IRequestHandler<CreateAllowanceCommand, CreateAllowanceCommand>
{
    private readonly IAllowancesRepository allowancesRepository;

    public CreateAllowanceCommandHandler(IAllowancesRepository allowancesRepository)
    {
        this.allowancesRepository = allowancesRepository;
    }

    public async Task<CreateAllowanceCommand> Handle(CreateAllowanceCommand request, CancellationToken cancellationToken)
    {
        var allowances = this.allowancesRepository.GetAll();
        if (allowances.Any(x => x.Name == request.Name))
        {
            throw new ConflictRequestException($"Department : '{request.Name}' already exists");
        }

        var entity = request.ToEntity();

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await allowancesRepository.InsertAsync(entity, cancellationToken);

        return request;
    }
}

