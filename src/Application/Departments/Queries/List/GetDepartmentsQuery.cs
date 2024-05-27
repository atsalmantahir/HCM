using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.Departments.Queries.Get;
using HumanResourceManagement.Application.EmployeeProfiles.Queries.Get;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Departments.Queries.List;

public record GetDepartmentsQuery(int organisationId) : PaginatedQuery, IRequest<PaginatedList<DepartmentVM>>;

public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, PaginatedList<DepartmentVM>>
{
    private readonly IDepartmentsRepository departmentsRepository;

    public GetDepartmentsQueryHandler(IDepartmentsRepository departmentsRepository)
    {
        this.departmentsRepository = departmentsRepository;
    }

    public async Task<PaginatedList<DepartmentVM>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var departments = this.departmentsRepository.GetAll(request.organisationId);
        var response = departments.ToDto();
        return await PaginatedList<DepartmentVM>.CreateAsync(response, request?.PageNumber, request?.PageSize);
    }
}
