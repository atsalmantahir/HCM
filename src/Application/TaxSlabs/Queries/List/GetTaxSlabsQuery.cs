using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.TaxSlabs.Queries.Get;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.TaxSlabs.Queries.List;

public record GetTaxSlabsQuery : PaginatedQuery, IRequest<PaginatedList<TaxSlabVM>>;
public class GetTaxSlabsQueryHandler : IRequestHandler<GetTaxSlabsQuery, PaginatedList<TaxSlabVM>>
{
    private readonly ITaxSlabsRepository repository;

    public GetTaxSlabsQueryHandler(ITaxSlabsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<PaginatedList<TaxSlabVM>> Handle(GetTaxSlabsQuery request, CancellationToken cancellationToken)
    {
        var taxSlabs = this.repository.GetAll();
        var response = taxSlabs.ToDto();

        return await PaginatedList<TaxSlabVM>.CreateAsync(response, request?.PageNumber, request?.PageSize);
    }
}
