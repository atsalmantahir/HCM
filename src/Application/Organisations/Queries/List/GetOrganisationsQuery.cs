using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.Holidays.Queries.Get;
using HumanResourceManagement.Application.Organisations.Queries.Get;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Organisations.Queries.List;

public record GetOrganisationsQuery : PaginatedQuery, IRequest<PaginatedList<OrganisationVM>>;

public class GetOrganisationsQueryHandler : IRequestHandler<GetOrganisationsQuery, PaginatedList<OrganisationVM>>
{
    private readonly IOrganisationsRepository repository;

    public GetOrganisationsQueryHandler(IOrganisationsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<PaginatedList<OrganisationVM>> Handle(GetOrganisationsQuery request, CancellationToken cancellationToken)
    {
        var organisations = this.repository.GetAll();
        var response = organisations.ToQueryOrganisationListDto();

        return await PaginatedList<OrganisationVM>.CreateAsync(response, request?.PageNumber, request?.PageSize);
    }
}
