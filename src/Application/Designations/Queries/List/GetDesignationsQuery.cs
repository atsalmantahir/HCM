using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.Departments.Queries.Get;
using HumanResourceManagement.Application.Designations.Queries.Get;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Designations.Queries.List;

public record GetDesignationsQuery(string departmentExternalIdentifier) : PaginatedQuery, IRequest<PaginatedList<DesignationVM>>;

public class GetDesignationsQueryHandler : IRequestHandler<GetDesignationsQuery, PaginatedList<DesignationVM>>
{
    private readonly IDesignationsRepository repository;

    public GetDesignationsQueryHandler(IDesignationsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<PaginatedList<DesignationVM>> Handle(GetDesignationsQuery request, CancellationToken cancellationToken)
    {
        var designations = this.repository.GetAll(request.departmentExternalIdentifier);
        var response = designations.ToDesignationListDto();

        return await PaginatedList<DesignationVM>.CreateAsync(response, request?.PageNumber, request?.PageSize);
    }
}