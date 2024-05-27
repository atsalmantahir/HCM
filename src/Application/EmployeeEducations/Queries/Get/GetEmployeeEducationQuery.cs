using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeEducations.Queries.Get;

public record GetEmployeeEducationQuery(int employeeExternalId, int id) : IRequest<EmployeeEducationVM>;

public class GetEmployeeEducationQueryHandler : IRequestHandler<GetEmployeeEducationQuery, EmployeeEducationVM>
{
    private readonly IEmployeeProfilesRepository repository;
    private readonly IEmployeeEducationsRepository educationsRepository;

    public GetEmployeeEducationQueryHandler(IEmployeeProfilesRepository repository, IEmployeeEducationsRepository educationsRepository)
    {
        this.repository = repository;
        this.educationsRepository = educationsRepository;
    }

    public async Task<EmployeeEducationVM> Handle(GetEmployeeEducationQuery request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.repository.GetAsync(request.employeeExternalId);
        if (employeeProfile == null) 
        {
            throw new EmployeeNotFoundException(request.employeeExternalId.ToString());
        }

        var employeeEducation = await this.educationsRepository.GetAsync(request.id);
        if (employeeEducation == null) 
        {
            throw new EmployeeEducationNotFoundException(request.id.ToString());
        }

        return employeeEducation.ToDto();
    }
}
