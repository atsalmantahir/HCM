using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.EmployeeProfiles.Queries.Get;
using HumanResourceManagement.Application.EmployeeProfiles.Queries.List;
using HumanResourceManagement.Application.Organisations.Queries.Get;
using HumanResourceManagement.Application.Payrolls.Queries.Get;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Payrolls.Queries.List;

public record GetPayrollsQuery : PaginatedQuery, IRequest<PaginatedList<PayrollVM>>;

public class GetPayrollsQueryHandler : IRequestHandler<GetPayrollsQuery, PaginatedList<PayrollVM>>
{
    private readonly IPayrollsRepository repository;

    public GetPayrollsQueryHandler(IPayrollsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<PaginatedList<PayrollVM>> Handle(GetPayrollsQuery request, CancellationToken cancellationToken)
    {
        var payrolls = this.repository.GetAll();
        var response = payrolls.ToPayrollsVMDto();

        return await PaginatedList<PayrollVM>.CreateAsync(response, request?.PageNumber, request?.PageSize);
    }
}
