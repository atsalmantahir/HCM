using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class TaxSlabsRepository : ITaxSlabsRepository
{
    private readonly IApplicationDbContext context;

    public TaxSlabsRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task DeleteAsync(TaxSlab entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.TaxSlabs.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TaxSlab> GetAsync(int id)
    {
        return await this
            .context
            .TaxSlabs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TaxSlabId == id && x.IsDeleted == false);
    }

    public async Task<TaxSlab> GetAsync(string externalIdentifier)
    {
        return await this
            .context
            .TaxSlabs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ExternalIdentifier == externalIdentifier && x.IsDeleted == false);
    }

    public IQueryable<TaxSlab> GetAll()
    {
        return this.context.TaxSlabs.Where(x => x.IsDeleted == false)
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task InsertAsync(TaxSlab entity, CancellationToken cancellationToken)
    {
        await this.context.TaxSlabs.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TaxSlab entity, CancellationToken cancellationToken)
    {
        this.context.TaxSlabs.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
