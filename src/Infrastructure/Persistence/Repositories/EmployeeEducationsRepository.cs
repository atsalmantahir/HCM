using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class EmployeeEducationsRepository : IEmployeeEducationsRepository
{
    private readonly IApplicationDbContext context;

    public EmployeeEducationsRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task DeleteAsync(EmployeeEducation entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.EmployeeEducations.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<EmployeeEducation> GetAsync(int id)
    {
        return await this
            .context
            .EmployeeEducations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.EmployeeEducationId == id && x.IsDeleted == false);
    }

    public async Task<IList<EmployeeEducation>> GetAllAsync()
    {
        return await this.context.EmployeeEducations.Where(x => x.IsDeleted == false).ToListAsync();
    }

    public async Task<IList<EmployeeEducation>> GetAllAsync(int employeeProfileId)
    {
        return await this.context.EmployeeEducations
            .Where(x => x.IsDeleted == false && x.EmployeeProfileId == employeeProfileId).ToListAsync();
    }

    public async Task InsertAsync(EmployeeEducation entity, CancellationToken cancellationToken)
    {
        await this.context.EmployeeEducations.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(EmployeeEducation entity, CancellationToken cancellationToken)
    {
        this.context.EmployeeEducations.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
