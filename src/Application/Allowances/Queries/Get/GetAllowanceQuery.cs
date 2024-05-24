using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Departments.Queries.Get;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Allowances.Queries.Get;

public record GetAllowanceQuery(string externalIdentifier) : IRequest<AllowanceVM>;


public class GetAllowanceQueryHandler : IRequestHandler<GetAllowanceQuery, AllowanceVM>
{
    private readonly IAllowancesRepository allowancesRepository;

    public GetAllowanceQueryHandler(IAllowancesRepository allowancesRepository)
    {
        this.allowancesRepository = allowancesRepository;
    }

    public async Task<AllowanceVM> Handle(GetAllowanceQuery request, CancellationToken cancellationToken)
    {
        var allowance = await this.allowancesRepository.GetAsync(request.externalIdentifier);
        if (allowance is null)
        {
            throw new DepartmentNotFoundException(request.externalIdentifier);
        }

        return allowance.ToDto();
    }
}