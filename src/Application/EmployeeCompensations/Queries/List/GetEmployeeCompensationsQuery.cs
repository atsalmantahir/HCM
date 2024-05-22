using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.Designations.Queries.Get;
using HumanResourceManagement.Application.EmployeeAttendences.Queries.Get;
using HumanResourceManagement.Application.EmployeeAttendences.Queries.List;
using HumanResourceManagement.Application.EmployeeCompensations.Queries.Get;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeCompensations.Queries.List;

public record GetEmployeeCompensationsQuery : PaginatedQuery, IRequest<PaginatedList<EmployeeCompensationVM>>;

public class GetEmployeeCompensationsQueryHandler : IRequestHandler<GetEmployeeCompensationsQuery, PaginatedList<EmployeeCompensationVM>>
{
    private readonly IEmployeeCompensationsRepository repository;

    public GetEmployeeCompensationsQueryHandler(IEmployeeCompensationsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<PaginatedList<EmployeeCompensationVM>> Handle(GetEmployeeCompensationsQuery request, CancellationToken cancellationToken)
    {
        var employeeCompensations = this.repository.GetAll();
        var response = employeeCompensations.ToDto();

        return await PaginatedList<EmployeeCompensationVM>.CreateAsync(response, request?.PageNumber, request?.PageSize);
    }
}
