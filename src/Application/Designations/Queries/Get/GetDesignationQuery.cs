using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Departments.Queries.Get;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace HumanResourceManagement.Application.Designations.Queries.Get;

public record GetDesignationQuery(int departmentId, int id) : IRequest<DesignationVM>;

public class GetDesignationQueryHandler : IRequestHandler<GetDesignationQuery, DesignationVM>
{
    private readonly IDesignationsRepository repository;

    public GetDesignationQueryHandler(IDesignationsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<DesignationVM> Handle(GetDesignationQuery request, CancellationToken cancellationToken)
    {
        var department = await this.repository.GetAsync(request.id);
        if (department is null) 
        {
            throw new DesignationNotFoundException(request.id.ToString());
        }
        return department.ToDto();
    }
}
