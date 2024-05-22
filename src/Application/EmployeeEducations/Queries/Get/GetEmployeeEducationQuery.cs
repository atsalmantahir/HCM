using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeEducations.Queries.Get;

public record GetEmployeeEducationQuery(string employeeExternalIdentifier, string externalIdentifier) : IRequest<EmployeeEducationVM>;

public class GetEmployeeEducationQueryHandler : IRequestHandler<GetEmployeeEducationQuery, EmployeeEducationVM>
{
    private readonly IEmployeeEducationsRepository repository;
    private readonly IEmployeeEducationsRepository educationsRepository;

    public GetEmployeeEducationQueryHandler(IEmployeeEducationsRepository repository, IEmployeeEducationsRepository educationsRepository)
    {
        this.repository = repository;
        this.educationsRepository = educationsRepository;
    }

    public async Task<EmployeeEducationVM> Handle(GetEmployeeEducationQuery request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.educationsRepository.GetAsync(request.employeeExternalIdentifier);
        if (employeeProfile == null) 
        {
            throw new EmployeeNotFoundException(request.employeeExternalIdentifier);
        }

        var employeeEducation = await this.repository.GetAsync(request.externalIdentifier);
        if (employeeEducation == null) 
        {
            throw new EmployeeEducationNotFoundException(request.externalIdentifier);
        }

        return employeeEducation.ToDto();
    }
}
