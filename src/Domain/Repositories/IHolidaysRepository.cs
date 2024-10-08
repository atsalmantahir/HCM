﻿namespace HumanResourceManagement.Domain.Repositories;

public interface IHolidaysRepository
{
    IQueryable<Holiday> GetAll();
    Task<Holiday> GetAsync(int id);
    Task InsertAsync(Holiday entity, CancellationToken cancellationToken);
    Task UpdateAsync(Holiday entity, CancellationToken cancellationToken);
    Task DeleteAsync(Holiday entity, CancellationToken cancellationToken);
}
