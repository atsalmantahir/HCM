using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class EmployeeProfilesRepository : IEmployeeProfilesRepository
{
    private readonly IApplicationDbContext context;

    public EmployeeProfilesRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task DeleteAsync(EmployeeProfile entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.EmployeeProfiles.Update(entity);

        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<EmployeeProfile> GetAsync(int id)
    {
        return await this
            .context
            .EmployeeProfiles
            .AsNoTracking()
            .Include(x => x.EmployeeLoans)
            .Include(x => x.EmployeeEducations)
            .Include(x => x.EmployeeExperiences)
            .Include(x => x.EmployeeCompensation)
            .Include(x => x.Designation)
                .ThenInclude(x => x.Department)
                .ThenInclude(x => x.Organisation)
            .FirstOrDefaultAsync(x => x.EmployeeProfileId == id && x.IsDeleted == false);
    }

    public async Task<EmployeeProfile> GetAsync(string externalIdentifier)
    {
        return await this
            .context
            .EmployeeProfiles
            .AsNoTracking()
            .Include(x => x.EmployeeLoans)
            .Include(x => x.EmployeeEducations)
            .Include(x => x.EmployeeExperiences)
            .Include(x => x.EmployeeCompensation)
            .Include(x => x.Designation)
                .ThenInclude(x => x.Department)
                .ThenInclude(x => x.Organisation)
            .FirstOrDefaultAsync(x => x.ExternalIdentifier == externalIdentifier && x.IsDeleted == false);
    }

    public IQueryable<EmployeeProfile> GetAll()
    {
        return this
            .context.EmployeeProfiles
            .Where(x => x.IsDeleted == false)
            .Include(x => x.EmployeeLoans)
            .Include(x => x.EmployeeEducations)
            .Include(x => x.EmployeeExperiences)
            .Include(x => x.Designation)
                .ThenInclude(x => x.Department)
                .ThenInclude(x => x.Organisation)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task<IList<EmployeeProfile>> GetAllByDesignationIdAsync(int designationId)
    {
        return await this
            .context
            .EmployeeProfiles
            .Where(x => x.DesignationId == designationId && x.IsDeleted == false)
            .ToListAsync();
    }

    public async Task InsertAsync(EmployeeProfile entity, CancellationToken cancellationToken)
    {
        await this
            .context
            .EmployeeProfiles
            .AddAsync(entity);

        await this
            .context
            .SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(EmployeeProfile entity, CancellationToken cancellationToken)
    {
        this.context
            .EmployeeProfiles
            .Update(entity);

        await this
            .context
            .SaveChangesAsync(cancellationToken);
    }
}
