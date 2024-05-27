using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class EmployeeExperiencesRepository : IEmployeeExperiencesRepository
{
    private readonly IApplicationDbContext context;

    public EmployeeExperiencesRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task DeleteAsync(EmployeeExperience entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.EmployeeExperiences.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<EmployeeExperience> GetAsync(int id)
    {
        return await this
            .context
            .EmployeeExperiences
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.EmployeeExperienceId == id && x.IsDeleted == false);
    }

    public IQueryable<EmployeeExperience> GetAll()
    {
        return this.context.EmployeeExperiences
            .Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }


    public IQueryable<EmployeeExperience> GetAll(int employeeProfileId)
    {
        return this.context.EmployeeExperiences
            .Where(x => x.IsDeleted == false && x.EmployeeProfileId == employeeProfileId)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }


    public async Task InsertAsync(EmployeeExperience entity, CancellationToken cancellationToken)
    {
        await this.context.EmployeeExperiences.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(EmployeeExperience entity, CancellationToken cancellationToken)
    {
        this.context.EmployeeExperiences.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
