using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Allowances.Queries.Get;

public record GetAllowanceQuery(int id) : IRequest<AllowanceVM>;


public class GetAllowanceQueryHandler : IRequestHandler<GetAllowanceQuery, AllowanceVM>
{
    private readonly IAllowancesRepository allowancesRepository;

    public GetAllowanceQueryHandler(IAllowancesRepository allowancesRepository)
    {
        this.allowancesRepository = allowancesRepository;
    }

    public async Task<AllowanceVM> Handle(GetAllowanceQuery request, CancellationToken cancellationToken)
    {
        var allowance = await this.allowancesRepository.GetAsync(request.id);
        if (allowance is null)
        {
            throw new DepartmentNotFoundException(request.id.ToString());
        }

        return allowance.ToDto();
    }
}