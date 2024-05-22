using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.EmployeeCompensations.Commands.Create;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.TaxSlabs.Commands.Create;

public record CreateTaxSlabCommand : IRequest<CreateTaxSlabCommand>
{
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTill { get; set; }
    public decimal MinimumIncome { get; set; }
    public decimal MaximumIncome { get; set; }
    public decimal BaseTax { get; set; }
    public decimal PercentageTax { get; set; }
    public decimal ExcessAmount { get; set; }
}

public class CreateTaxSlabCommandHandler : IRequestHandler<CreateTaxSlabCommand, CreateTaxSlabCommand>
{
    private readonly ITaxSlabsRepository taxSlabsRepository;

    public CreateTaxSlabCommandHandler(ITaxSlabsRepository taxSlabsRepository)
    {
        this.taxSlabsRepository = taxSlabsRepository;
    }

    public async Task<CreateTaxSlabCommand> Handle(CreateTaxSlabCommand request, CancellationToken cancellationToken)
    {
        var entity = request.ToEntity();

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await taxSlabsRepository.InsertAsync(entity, cancellationToken);

        return request;
    }
}