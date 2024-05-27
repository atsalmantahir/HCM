using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Designations.Queries.Get;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeAttendences.Queries.Get;

public record GetEmployeeAttendenceQuery(int employeeProfileId, int Id) : IRequest<EmployeeAttendenceVM>;

public class GetEmployeeAttendenceQueryHandler : IRequestHandler<GetEmployeeAttendenceQuery, EmployeeAttendenceVM>
{
    private readonly IEmployeeAttendencesRepository repository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;

    public GetEmployeeAttendenceQueryHandler(IEmployeeAttendencesRepository repository, IEmployeeProfilesRepository employeeProfilesRepository)
    {
        this.repository = repository;
        this.employeeProfilesRepository = employeeProfilesRepository;
    }

    public async Task<EmployeeAttendenceVM> Handle(GetEmployeeAttendenceQuery request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeProfilesRepository.GetAsync(request.employeeProfileId);
        if (employeeProfile is null) 
        {
            throw new EmployeeNotFoundException(request.employeeProfileId.ToString());
        }

        var employeeAttendance = await this.repository.GetAsync(request.Id);
        if (employeeAttendance is null)
        {
            throw new EmployeeAttendenceNotFoundException(request.Id.ToString());
        }

        return employeeAttendance.ToDto();
    }
}
