using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.EmployeeEducations.Queries.Get;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeExperiences.Queries.Get;

public record GetEmployeeExperienceQuery(int employeeProfileId, int id) : IRequest<EmployeeExperienceVM>;

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
        var employeeProfile = await this.employeeExperiencesRepository.GetAsync(request.employeeProfileId);
        if (employeeProfile == null) 
        {
            throw new EmployeeNotFoundException(request.employeeProfileId.ToString());
        }

        var employeeExperience = await this.repository.GetAsync(request.id);
        if (employeeExperience == null)
        {
            throw new EmployeeExperienceNotFoundException(request.id.ToString());
        }

        return employeeExperience.ToDto();
    }
}