using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class EmployeeAllowancesRepository : IEmployeeAllowancesRepository
{
    private readonly IApplicationDbContext context;

    public EmployeeAllowancesRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task DeleteAsync(EmployeeAllowance entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.EmployeeAllowances.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<EmployeeAllowance> GetAll()
    {
        return this.context.EmployeeAllowances
            .Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task<EmployeeAllowance> GetAsync(int id)
    {
        return await this
            .context
            .EmployeeAllowances
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.AllowanceId == id && x.IsDeleted == false);
    }

    //public async Task<EmployeeAllowance> GetAsync(string externalIdentifier)
    //{
    //    return await this
    //        .context
    //        .EmployeeAllowances
    //        .AsNoTracking()
    //        .FirstOrDefaultAsync(x => x.ExternalIdentifier == externalIdentifier && x.IsDeleted == false);
    //}

    public async Task InsertAsync(EmployeeAllowance entity, CancellationToken cancellationToken)
    {
        await this.context.EmployeeAllowances.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(EmployeeAllowance entity, CancellationToken cancellationToken)
    {
        this.context.EmployeeAllowances.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
