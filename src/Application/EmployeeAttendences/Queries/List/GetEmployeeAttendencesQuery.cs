using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.Designations.Queries.Get;
using HumanResourceManagement.Application.Designations.Queries.List;
using HumanResourceManagement.Application.EmployeeAttendences.Queries.Get;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeAttendences.Queries.List;

public record GetEmployeeAttendencesQuery : PaginatedQuery, IRequest<PaginatedList<EmployeeAttendenceVM>>;

public class GetEmployeeAttendencesQueryHandler : IRequestHandler<GetEmployeeAttendencesQuery, PaginatedList<EmployeeAttendenceVM>>
{
    private readonly IEmployeeAttendencesRepository repository;

    public GetEmployeeAttendencesQueryHandler(IEmployeeAttendencesRepository repository)
    {
        this.repository = repository;
    }

    public async Task<PaginatedList<EmployeeAttendenceVM>> Handle(GetEmployeeAttendencesQuery request, CancellationToken cancellationToken)
    {
        var employeeAttendances = this.repository.GetAll();
        var response = employeeAttendances.ToDto();

        return await PaginatedList<EmployeeAttendenceVM>.CreateAsync(response, request?.PageNumber, request?.PageSize);
    }
}
