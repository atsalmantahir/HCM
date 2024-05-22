using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class OrganisationsRepository : IOrganisationsRepository
{
    private readonly IApplicationDbContext context;

    public OrganisationsRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task DeleteAsync(Organisation entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.Organisations.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Organisation> GetAsync(int id)
    {
        return await this
            .context
            .Organisations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.OrganisationId == id && x.IsDeleted == false);
    }

    public async Task<Organisation> GetAsync(string externalIdentifier)
    {
        return await this
            .context
            .Organisations
            .Include(x => x.Departments)
                .ThenInclude(x => x.Designations)
                .ThenInclude(x => x.EmployeeProfiles)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ExternalIdentifier == externalIdentifier && x.IsDeleted == false);
    }

    public IQueryable<Organisation> GetAll()
    {
        return this.context.Organisations.Where(x => x.IsDeleted == false)
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task InsertAsync(Organisation entity, CancellationToken cancellationToken)
    {
        await this.context.Organisations.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Organisation entity, CancellationToken cancellationToken)
    {
        this.context.Organisations.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
