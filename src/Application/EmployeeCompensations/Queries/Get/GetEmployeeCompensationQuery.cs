using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.EmployeeAttendences.Queries.Get;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeCompensations.Queries.Get;

public record GetEmployeeCompensationQuery(string employeeExternalIdentifier, string externalIdentifier) : IRequest<EmployeeCompensationVM>;

public class GetEmployeeCompensationQueryHandler : IRequestHandler<GetEmployeeCompensationQuery, EmployeeCompensationVM>
{
    private readonly IEmployeeCompensationsRepository repository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;

    public GetEmployeeCompensationQueryHandler(
        IEmployeeCompensationsRepository repository, 
        IEmployeeProfilesRepository employeeProfilesRepository)
    {
        this.repository = repository;
        this.employeeProfilesRepository = employeeProfilesRepository;
    }

    public async Task<EmployeeCompensationVM> Handle(GetEmployeeCompensationQuery request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeProfilesRepository.GetAsync(request.employeeExternalIdentifier);
        if (employeeProfile is null) 
        {
            throw new EmployeeNotFoundException(request.employeeExternalIdentifier);
        }

        var employeeCompensation = await repository.GetAsync(request.externalIdentifier);
        if (employeeCompensation is null) 
        {
            throw new EmployeeCompensationNotFoundException(request.externalIdentifier);
        }

        return employeeCompensation.ToDto();
    }
}
