using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.EmployeeEducations.Queries.Get;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeExperiences.Queries.Get;

public record GetEmployeeExperienceQuery(string employeeExternalIdentifier, string externalIdentifier) : IRequest<EmployeeExperienceVM>;

public class GetEmployeeExperienceQueryHandler : IRequestHandler<GetEmployeeExperienceQuery, EmployeeExperienceVM>
{
    private readonly IEmployeeExperiencesRepository repository;
    private readonly IEmployeeExperiencesRepository employeeExperiencesRepository;

    public GetEmployeeExperienceQueryHandler(IEmployeeExperiencesRepository repository, IEmployeeExperiencesRepository employeeExperiencesRepository)
    {
        this.repository = repository;
        this.employeeExperiencesRepository = employeeExperiencesRepository;
    }

    public async Task<EmployeeExperienceVM> Handle(GetEmployeeExperienceQuery request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeExperiencesRepository.GetAsync(request.employeeExternalIdentifier);
        if (employeeProfile == null) 
        {
            throw new EmployeeNotFoundException(request.employeeExternalIdentifier);
        }

        var employeeExperience = await this.repository.GetAsync(request.externalIdentifier);
        if (employeeExperience == null)
        {
            throw new EmployeeExperienceNotFoundException(request.externalIdentifier);
        }

        return employeeExperience.ToDto();
    }
}