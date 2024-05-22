using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.EmployeeCompensations.Queries.Get;
using HumanResourceManagement.Application.EmployeeExperiences.Queries.Get;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeExperiences.Queries.List;

public record GetEmployeeExperiencesQuery : PaginatedQuery, IRequest<PaginatedList<EmployeeExperienceVM>>;

public class GetEmployeeExperiencesQueryHandler : IRequestHandler<GetEmployeeExperiencesQuery, PaginatedList<EmployeeExperienceVM>>
{
    private readonly IEmployeeExperiencesRepository repository;

    public GetEmployeeExperiencesQueryHandler(IEmployeeExperiencesRepository repository)
    {
        this.repository = repository;
    }

    public async Task<PaginatedList<EmployeeExperienceVM>> Handle(GetEmployeeExperiencesQuery request, CancellationToken cancellationToken)
    {
        var employeeExperiences = this.repository.GetAll();
        var response = employeeExperiences.ToDto();

        return await PaginatedList<EmployeeExperienceVM>.CreateAsync(response, request?.PageNumber, request?.PageSize);
    }
}