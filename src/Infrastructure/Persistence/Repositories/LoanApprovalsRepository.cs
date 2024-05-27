using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class LoanApprovalsRepository : ILoanApprovalsRepository
{
    private readonly IApplicationDbContext context;

    public LoanApprovalsRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task DeleteAsync(LoanApproval entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.LoanApprovals.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<LoanApproval> GetAsync(int id)
    {
        return await this
            .context
            .LoanApprovals
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.LoanApprovalID == id && x.IsDeleted == false);
    }

    public IQueryable<LoanApproval> GetAll()
    {
        return this.context.LoanApprovals
            .Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task InsertAsync(LoanApproval entity, CancellationToken cancellationToken)
    {
        await this.context.LoanApprovals.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(LoanApproval entity, CancellationToken cancellationToken)
    {
        this.context.LoanApprovals.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
