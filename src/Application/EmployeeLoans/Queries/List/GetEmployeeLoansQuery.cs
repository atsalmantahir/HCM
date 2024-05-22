using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.EmployeeLoans.Queries.Get;
using HumanResourceManagement.Application.EmployeeProfiles.Queries.Get;
using HumanResourceManagement.Application.EmployeeProfiles.Queries.List;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeLoans.Queries.List;

public record GetEmployeeLoansQuery : PaginatedQuery, IRequest<PaginatedList<EmployeeLoanVM>>;

public class GetEmployeeLoansQueryHandler : IRequestHandler<GetEmployeeLoansQuery, PaginatedList<EmployeeLoanVM>>
{
    private readonly IEmployeeLoansRepository employeeLoansRepository;

    public GetEmployeeLoansQueryHandler(IEmployeeLoansRepository employeeLoansRepository)
    {
        this.employeeLoansRepository = employeeLoansRepository;
    }

    public async Task<PaginatedList<EmployeeLoanVM>> Handle(GetEmployeeLoansQuery request, CancellationToken cancellationToken)
    {
        var employeeLoans = this.employeeLoansRepository.GetAll();
        var response = employeeLoans.ToDto();
        return await PaginatedList<EmployeeLoanVM>.CreateAsync(response, request?.PageNumber, request?.PageSize);
    }
}