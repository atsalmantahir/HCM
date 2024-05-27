using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.EmployeeProfiles.Commands.Update;
using HumanResourceManagement.Application.TaxSlabs.Commands.Create;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.TaxSlabs.Commands.Update;

public record UpdateTaxSlabCommand : IRequest<UpdateTaxSlabCommand>
{
    public int Id { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTill { get; set; }
    public decimal MinimumIncome { get; set; }
    public decimal MaximumIncome { get; set; }
    public decimal BaseTax { get; set; }
    public decimal PercentageTax { get; set; }
    public decimal ExcessAmount { get; set; }
}

public class UpdateTaxSlabCommandHandler : IRequestHandler<UpdateTaxSlabCommand, UpdateTaxSlabCommand>
{
    private readonly ITaxSlabsRepository taxSlabsRepository;

    public UpdateTaxSlabCommandHandler(ITaxSlabsRepository taxSlabsRepository)
    {
        this.taxSlabsRepository = taxSlabsRepository;
    }

    public async Task<UpdateTaxSlabCommand> Handle(UpdateTaxSlabCommand request, CancellationToken cancellationToken)
    {
        var taxSlab = await this.taxSlabsRepository.GetAsync(request.Id);

        if (taxSlab is null) 
        {
            return null;
        }

        var entity = request.ToEntity(taxSlab.TaxSlabId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await taxSlabsRepository.UpdateAsync(entity, cancellationToken);

        return request;
    }
}

public static class UpdateTaxSlabCommandExtention
{
    public static UpdateTaxSlabCommand StructureRequest(
        this UpdateTaxSlabCommand request,
        int id)
    {
        return new UpdateTaxSlabCommand
        {
            Id = id,
            ValidFrom = request.ValidFrom,
            ValidTill = request.ValidTill,
            MinimumIncome = request.MinimumIncome,
            MaximumIncome = request.MaximumIncome,
            BaseTax = request.BaseTax,
            PercentageTax = request.PercentageTax,
            ExcessAmount = request.ExcessAmount,
        };
    }
}
