using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Departments.Queries.Get;

public record GetDepartmentQuery(string organistaionExternalIdentifier, string externalIdentifier) : IRequest<DepartmentVM>;

public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, DepartmentVM>
{
    private readonly IDepartmentsRepository departmentsRepository;

    public GetDepartmentQueryHandler(IDepartmentsRepository departmentsRepository)
    {
        this.departmentsRepository = departmentsRepository;
    }

    public async Task<DepartmentVM> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
    {
        var department = await this.departmentsRepository.GetAsync(
            request.organistaionExternalIdentifier,
            request.externalIdentifier);
        if (department is null) 
        {
            throw new DepartmentNotFoundException(request.externalIdentifier);
        }

        return department.ToDto();
    }
}