using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class HolidaysRepository : IHolidaysRepository
{
    private readonly IApplicationDbContext context;

    public HolidaysRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task DeleteAsync(Holiday entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.Holidays.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Holiday> GetAsync(int id)
    {
        return await this
            .context
            .Holidays
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.HolidayId == id && x.IsDeleted == false);
    }

    public async Task<Holiday> GetAsync(string externalIdentifier)
    {
        return await this
            .context
            .Holidays
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ExternalIdentifier == externalIdentifier && x.IsDeleted == false);
    }

    public IQueryable<Holiday> GetAll()
    {
        return this.context.Holidays.Where(x => x.IsDeleted == false)
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt); 
    }

    public async Task InsertAsync(Holiday entity, CancellationToken cancellationToken)
    {
        await this.context.Holidays.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Holiday entity, CancellationToken cancellationToken)
    {
        this.context.Holidays.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
