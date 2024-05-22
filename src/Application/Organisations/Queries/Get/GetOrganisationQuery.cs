using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Departments.Queries.Get;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Organisations.Queries.Get;

public record GetOrganisationQuery(string externalIdentifier) : IRequest<OrganisationVM>;

public class GetOrganisationQueryHandler : IRequestHandler<GetOrganisationQuery, OrganisationVM>
{
    private readonly IOrganisationsRepository repository;

    public GetOrganisationQueryHandler(IOrganisationsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<OrganisationVM> Handle(GetOrganisationQuery request, CancellationToken cancellationToken)
    {
        var organisation = await this.repository.GetAsync(request.externalIdentifier);
        return organisation.ToQueryOrganisationDto();
    }
}