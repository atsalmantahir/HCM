using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    private readonly IApplicationDbContext context;

    public DepartmentsRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task DeleteAsync(Department entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.Departments.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Department> GetAsync(int id)
    {
        return await this
            .context
            .Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.DepartmentId == id && x.IsDeleted == false);
    }

    public async Task<Department> GetAsync(string organisationExternalIdentifier, string externalIdentifier)
    {
        return await this
            .context
            .Departments
            .Include(y => y.Organisation)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Organisation.ExternalIdentifier == organisationExternalIdentifier
            && x.ExternalIdentifier == externalIdentifier
            && x.IsDeleted == false);
    }

    public async Task<Department> GetAsync(string externalIdentifier)
    {
        return await this
            .context
            .Departments
            .Include(y => y.Organisation)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ExternalIdentifier == externalIdentifier
            && x.IsDeleted == false);
    }

    public IQueryable<Department> GetAll(string organisationExternalIdentifier)
    {
        return this.context
            .Departments
            .Include(x => x.Organisation)
            .Where(x => x.Organisation.ExternalIdentifier == organisationExternalIdentifier && x.IsDeleted == false)
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task InsertAsync(Department entity, CancellationToken cancellationToken)
    {
        await this.context.Departments.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Department entity, CancellationToken cancellationToken)
    {
        this.context.Departments.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
