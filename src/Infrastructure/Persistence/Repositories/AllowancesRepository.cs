using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class AllowancesRepository : IAllowancesRepository
{
    private readonly IApplicationDbContext context;

    public AllowancesRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task DeleteAsync(Allowance entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.Allowances.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<Allowance> GetAll()
    {
        return this.context.Allowances
            .Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task<Allowance> GetAsync(int id)
    {
        return await this
            .context
            .Allowances
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.AllowanceId == id && x.IsDeleted == false);
    }


    public async Task InsertAsync(Allowance entity, CancellationToken cancellationToken)
    {
        await this.context.Allowances.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Allowance entity, CancellationToken cancellationToken)
    {
        this.context.Allowances.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
