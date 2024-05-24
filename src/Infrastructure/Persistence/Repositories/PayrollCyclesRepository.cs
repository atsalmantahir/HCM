using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class PayrollCyclesRepository : IPayrollCyclesRepository
{
    private readonly IApplicationDbContext context;

    public PayrollCyclesRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task DeleteAsync(PayrollCycle entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.PayrollCycles.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<PayrollCycle> GetAsync(int id)
    {
        return await this
            .context
            .PayrollCycles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PayrollCycleId == id && x.IsDeleted == false);
    }

    public async Task<PayrollCycle> GetAsync(string externalIdentifier)
    {
        return await this
            .context
            .PayrollCycles
            //.Include(x => x.PayrollStatus)
            .Include(x => x.Payrolls)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ExternalIdentifier == externalIdentifier && x.IsDeleted == false);
    }

    public IQueryable<PayrollCycle> GetAll()
    {
        return this.context.PayrollCycles
            .Where(x => x.IsDeleted == false)
            .Include(x => x.Payrolls)
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task InsertAsync(PayrollCycle entity, CancellationToken cancellationToken)
    {
        await this.context.PayrollCycles.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task InsertListAsync(IList<PayrollCycle> entity, CancellationToken cancellationToken)
    {
        await this.context.PayrollCycles.AddRangeAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PayrollCycle entity, CancellationToken cancellationToken)
    {
        this.context.PayrollCycles.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
