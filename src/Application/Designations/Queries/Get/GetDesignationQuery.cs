using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Departments.Queries.Get;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace HumanResourceManagement.Application.Designations.Queries.Get;

public record GetDesignationQuery(string departmentExternalIdentifier, string externalIdentifier) : IRequest<DesignationVM>;

public class GetDesignationQueryHandler : IRequestHandler<GetDesignationQuery, DesignationVM>
{
    private readonly IDesignationsRepository repository;

    public GetDesignationQueryHandler(IDesignationsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<DesignationVM> Handle(GetDesignationQuery request, CancellationToken cancellationToken)
    {
        var department = await this.repository.GetAsync(request.departmentExternalIdentifier, request.externalIdentifier);
        if (department is null) 
        {
            throw new DesignationNotFoundException(request.externalIdentifier);
        }
        return department.ToDto();
    }
}
