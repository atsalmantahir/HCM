using HumanResourceManagement.Application.Allowances.Queries.Get;
using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.Departments.Queries.Get;
using HumanResourceManagement.Application.Departments.Queries.List;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Allowances.Queries.List;

public record GetAllowancesQuery : PaginatedQuery, IRequest<PaginatedList<AllowanceVM>>;

public class GetAllowancesQueryHandler : IRequestHandler<GetAllowancesQuery, PaginatedList<AllowanceVM>>
{
    private readonly IAllowancesRepository allowancesRepository;

    public GetAllowancesQueryHandler(IAllowancesRepository allowancesRepository)
    {
        this.allowancesRepository = allowancesRepository;
    }

    public async Task<PaginatedList<AllowanceVM>> Handle(GetAllowancesQuery request, CancellationToken cancellationToken)
    {
        var allowances = this.allowancesRepository.GetAll();
        var response = allowances.ToDto();
        return await PaginatedList<AllowanceVM>.CreateAsync(response, request?.PageNumber, request?.PageSize);
    }
}
