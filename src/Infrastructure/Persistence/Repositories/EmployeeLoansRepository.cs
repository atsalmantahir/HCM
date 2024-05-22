using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class EmployeeLoansRepository : IEmployeeLoansRepository
{
    private readonly IApplicationDbContext context;

    public EmployeeLoansRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public IQueryable<EmployeeLoan> GetAll()
    {
        return this.context.EmployeeLoans
            .Where(x => x.IsDeleted == false)
            .Include(x => x.LoanGuarantors)
            .Include(x => x.LoanApprovals)
            .Include(x => x.EmployeeProfile)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task DeleteAsync(EmployeeLoan entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.EmployeeLoans.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<EmployeeLoan> GetAsync(int id)
    {
        return await this
            .context
            .EmployeeLoans
            .Include(x => x.EmployeeProfile)
            .Include(x => x.LoanGuarantors)
            .Include(x => x.LoanApprovals)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.EmployeeLoanID == id && x.IsDeleted == false);
    }

    public async Task<EmployeeLoan> GetAsync(string externalIdentifier)
    {
        return await this
            .context
            .EmployeeLoans
            .Include(x => x.EmployeeProfile)
            .Include(x => x.LoanApprovals)
            .Include(x => x.LoanGuarantors)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ExternalIdentifier == externalIdentifier && x.IsDeleted == false);
    }

    public async Task InsertAsync(EmployeeLoan entity, CancellationToken cancellationToken)
    {
        await this.context.EmployeeLoans.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(EmployeeLoan entity, CancellationToken cancellationToken)
    {
        this.context.EmployeeLoans.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
