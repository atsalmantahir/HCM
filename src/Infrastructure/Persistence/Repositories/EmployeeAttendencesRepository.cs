﻿using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class EmployeeAttendencesRepository : IEmployeeAttendencesRepository
{
    private readonly IApplicationDbContext context;

    public EmployeeAttendencesRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task DeleteAsync(EmployeeAttendance entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.EmployeeAttendances.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<EmployeeAttendance> GetAsync(int id)
    {
        return await this
            .context
            .EmployeeAttendances
            .Include(x => x.EmployeeProfile)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.EmployeeAttendanceId == id && x.IsDeleted == false);
    }

    public IQueryable<EmployeeAttendance> GetAll()
    {
        return this.context.EmployeeAttendances
            .Include(x => x.EmployeeProfile)
            .Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task<List<EmployeeAttendance>> GetAllAsync(int month, int year)
    {
        return await this.context.EmployeeAttendances
            .Where(x => x.AttendanceDate.Month == month && x.AttendanceDate.Year == year && x.IsDeleted == false)
            .ToListAsync();
    }

    public IQueryable<EmployeeAttendance> GetAll(int employeeProfileId)
    {
        return this.context.EmployeeAttendances
            .Include(x => x.EmployeeProfile)
            .Where(x => x.IsDeleted == false && x.EmployeeProfileId == employeeProfileId)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task InsertAsync(EmployeeAttendance entity, CancellationToken cancellationToken)
    {
        await this.context.EmployeeAttendances.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(EmployeeAttendance entity, CancellationToken cancellationToken)
    {
        this.context.EmployeeAttendances.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
