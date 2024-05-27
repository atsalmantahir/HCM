using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.TaxSlabs.Queries.Get;

public record GetTaxSlabQuery(int id) : IRequest<TaxSlabVM>;

public class GetTaxSlabQueryHandler : IRequestHandler<GetTaxSlabQuery, TaxSlabVM>
{
    private readonly ITaxSlabsRepository repository;

    public GetTaxSlabQueryHandler(ITaxSlabsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<TaxSlabVM> Handle(GetTaxSlabQuery request, CancellationToken cancellationToken)
    {
        var taxSlab = await this.repository.GetAsync(request.id);
        return taxSlab.ToDto();
    }
}