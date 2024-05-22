using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.EmployeeCompensations.Queries.Get;
using HumanResourceManagement.Application.EmployeeCompensations.Queries.List;
using HumanResourceManagement.Application.EmployeeEducations.Queries.Get;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.EmployeeEducations.Queries.List;

public class GetEmployeeEducationsQuery : IRequest<IList<EmployeeEducationVM>>;

public class GetEmployeeEducationsQueryHandler : IRequestHandler<GetEmployeeEducationsQuery, IList<EmployeeEducationVM>>
{
    private readonly IEmployeeEducationsRepository repository;

    public GetEmployeeEducationsQueryHandler(IEmployeeEducationsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IList<EmployeeEducationVM>> Handle(GetEmployeeEducationsQuery request, CancellationToken cancellationToken)
    {
        var employeeEducations = await this.repository.GetAllAsync();
        return employeeEducations.ToDto();
    }
}