using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class EmployeeCompensationsRepository : IEmployeeCompensationsRepository
{
    private readonly IApplicationDbContext context;

    public EmployeeCompensationsRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task DeleteAsync(EmployeeCompensation entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.EmployeeCompensations.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<EmployeeCompensation> GetAsync(int id)
    {
        return await this
            .context
            .EmployeeCompensations
            .Include(x => x.EmployeeProfile)
            .Include(x => x.EmployeeAllowances)
                .ThenInclude(x => x.Allowance)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.EmployeeCompensationId == id);
    }

    public async Task<EmployeeCompensation> GetByEmployeeProfileAsync(int employeeProfileId) 
    {
        return await this
            .context
            .EmployeeCompensations
            .Include(x => x.EmployeeProfile)
            .Include(x => x.EmployeeAllowances)
                .ThenInclude(x => x.Allowance)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.EmployeeProfileId == employeeProfileId);
    }

    public IQueryable<EmployeeCompensation> GetAll()
    {
        return this.context.EmployeeCompensations
            .Where(x => x.IsDeleted == false)
            .Include(x => x.EmployeeProfile)
            .Include(x => x.EmployeeAllowances)
                .ThenInclude(x => x.Allowance)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public IQueryable<EmployeeCompensation> GetAll(int employeeProfileId)
    {
        return this.context.EmployeeCompensations
            .Where(x => x.IsDeleted == false && x.EmployeeProfileId == employeeProfileId)
            .Include(x => x.EmployeeProfile)
            .Include(x => x.EmployeeAllowances)
                .ThenInclude(x => x.Allowance)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task InsertAsync(EmployeeCompensation entity, CancellationToken cancellationToken)
    {
        await this.context.EmployeeCompensations.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(EmployeeCompensation entity, CancellationToken cancellationToken)
    {
        this.context.EmployeeCompensations.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
