using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeLoans.Queries.Get;

public record GetEmployeeLoanQuery(int id) : IRequest<EmployeeLoanVM>;

public class GetEmployeeLoanQueryHandler : IRequestHandler<GetEmployeeLoanQuery, EmployeeLoanVM>
{
    private readonly IEmployeeLoansRepository employeeLoansRepository;
    private readonly IEmployeeExperiencesRepository employeeExperiencesRepository;

    public GetEmployeeLoanQueryHandler(IEmployeeLoansRepository employeeLoansRepository, IEmployeeExperiencesRepository employeeExperiencesRepository)
    {
        this.employeeLoansRepository = employeeLoansRepository;
        this.employeeExperiencesRepository = employeeExperiencesRepository;
    }

    public async Task<EmployeeLoanVM> Handle(GetEmployeeLoanQuery request, CancellationToken cancellationToken)
    {
        //var employeeProfile = await this.employeeExperiencesRepository.GetAsync(request.externalIdentifier);
        //if (employeeProfile == null)
        //{
        //    throw new EmployeeNotFoundException(request.externalIdentifier);
        //}

        var employeeLoan = await this.employeeLoansRepository.GetAsync(request.id);
        if (employeeLoan == null)
        {
            throw new EmployeeExperienceNotFoundException(request.id.ToString());
        }

        return employeeLoan.ToDto();
    }
}