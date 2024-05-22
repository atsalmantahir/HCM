using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class DesignationsRepository : IDesignationsRepository
{
    private readonly IApplicationDbContext context;

    public DesignationsRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task DeleteAsync(Designation entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.Designations.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Designation> GetAsync(int id)
    {
        return await this
            .context
            .Designations
            .Include(x => x.Department)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.DesignationId == id && x.IsDeleted == false);
    }

    public async Task<Designation> GetAsync(string departmentExternalIdentifier, string externalIdentifier)
    {
        return await this
            .context
            .Designations
            .Include(x => x.Department)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Department.ExternalIdentifier == departmentExternalIdentifier
            && x.ExternalIdentifier == externalIdentifier
            && x.IsDeleted == false);
    }

    public async Task<Designation> GetAsync(string externalIdentifier)
    {
        return await this
            .context
            .Designations
            .Include(x => x.Department)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ExternalIdentifier == externalIdentifier && x.IsDeleted == false);
    }

    public IQueryable<Designation> GetAll(string departmentExternalIdentifier)
    {
        return this.context.Designations
            .Include(x => x.Department)
            .Where(x => x.Department.ExternalIdentifier == departmentExternalIdentifier && x.IsDeleted == false)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task InsertAsync(Designation entity, CancellationToken cancellationToken)
    {
        await this.context.Designations.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Designation entity, CancellationToken cancellationToken)
    {
        this.context.Designations.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
