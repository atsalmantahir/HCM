using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.EmployeeExperiences.Queries.Get;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeProfiles.Queries.Get;

public record GetEmployeeProfileQuery(string externalIdentifier) : IRequest<EmployeeProfileVM>;

public class GetEmployeeProfileQueryHandler : IRequestHandler<GetEmployeeProfileQuery, EmployeeProfileVM>
{
    private readonly IEmployeeProfilesRepository repository;

    public GetEmployeeProfileQueryHandler(IEmployeeProfilesRepository repository)
    {
        this.repository = repository;
    }

    public async Task<EmployeeProfileVM> Handle(GetEmployeeProfileQuery request, CancellationToken cancellationToken)
    {
        var employeeProfile = await repository.GetAsync(request.externalIdentifier);
        if (employeeProfile is null) 
        {
            throw new EmployeeNotFoundException(request.externalIdentifier);
        }

        return employeeProfile.ToDto();
    }
}
