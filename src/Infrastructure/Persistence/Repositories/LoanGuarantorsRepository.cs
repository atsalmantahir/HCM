using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class LoanGuarantorsRepository : ILoanGuarantorsRepository
{
    private readonly IApplicationDbContext context;

    public LoanGuarantorsRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public IQueryable<LoanGuarantor> GetAll()
    {
        return this.context.LoanGuarantors
            .Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task DeleteAsync(LoanGuarantor entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.LoanGuarantors.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<LoanGuarantor> GetAsync(int id)
    {
        return await this
            .context
            .LoanGuarantors
            .Where(x => x.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.LoanGuarantorID == id && x.IsDeleted == false);
    }

    public async Task<LoanGuarantor> GetAsync(string externalIdentifier)
    {
        return await this
            .context
            .LoanGuarantors
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ExternalIdentifier == externalIdentifier && x.IsDeleted == false);
    }

    public async Task InsertAsync(LoanGuarantor entity, CancellationToken cancellationToken)
    {
        await this.context.LoanGuarantors.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(LoanGuarantor entity, CancellationToken cancellationToken)
    {
        this.context.LoanGuarantors.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
