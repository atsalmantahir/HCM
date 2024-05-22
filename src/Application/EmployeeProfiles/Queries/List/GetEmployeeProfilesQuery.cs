using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.EmployeeExperiences.Queries.Get;
using HumanResourceManagement.Application.EmployeeExperiences.Queries.List;
using HumanResourceManagement.Application.EmployeeProfiles.Queries.Get;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeProfiles.Queries.List;

public record GetEmployeeProfilesQuery : PaginatedQuery, IRequest<PaginatedList<EmployeeProfileVM>> 
{

}

public class GetEmployeeProfilesQueryHandler : IRequestHandler<GetEmployeeProfilesQuery, PaginatedList<EmployeeProfileVM>>
{
    private readonly IEmployeeProfilesRepository repository;

    public GetEmployeeProfilesQueryHandler(IEmployeeProfilesRepository repository)
    {
        this.repository = repository;
    }

    public async Task<PaginatedList<EmployeeProfileVM>> Handle(GetEmployeeProfilesQuery request, CancellationToken cancellationToken)
    {
        var employeeProfiles = this.repository.GetAll();
        var response = employeeProfiles.ToDto();
        return await PaginatedList<EmployeeProfileVM>.CreateAsync(response, request?.PageNumber, request?.PageSize);
    }
}
