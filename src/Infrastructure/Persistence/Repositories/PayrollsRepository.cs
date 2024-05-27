using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class PayrollsRepository : IPayrollsRepository
{
    private readonly IApplicationDbContext context;

    public PayrollsRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task DeleteAsync(Payroll entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.Payrolls.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Payroll> GetAsync(int id)
    {
        return await this
            .context
            .Payrolls
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PayrollId == id && x.IsDeleted == false);
    }

    public IQueryable<Payroll> GetAll()
    {
        return this.context.Payrolls
            .Where(x => x.IsDeleted == false)
            .Include(x => x.EmployeeProfile)
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task InsertAsync(Payroll entity, CancellationToken cancellationToken)
    {
        await this.context.Payrolls.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task InsertListAsync(IList<Payroll> payrolls, CancellationToken cancellationToken)
    {
        await this.context.Payrolls.AddRangeAsync(payrolls);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Payroll entity, CancellationToken cancellationToken)
    {
        this.context.Payrolls.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public Task<Payroll> GetAsync(int employeeProfileId, int year, int month)
    {
        throw new NotImplementedException();
    }
}
