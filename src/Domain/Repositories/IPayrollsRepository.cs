using System.Collections.Generic;

namespace HumanResourceManagement.Domain.Repositories;

public interface IPayrollsRepository
{
    IQueryable<Payroll> GetAll();
    Task<Payroll> GetAsync(int id);
    Task<Payroll> GetAsync(int employeeProfileId, int year, int month);
    Task InsertAsync(Payroll entity, CancellationToken cancellationToken);
    Task InsertListAsync(IList<Payroll> payrolls, CancellationToken cancellationToken);
    Task UpdateAsync(Payroll entity, CancellationToken cancellationToken);
    Task DeleteAsync(Payroll entity, CancellationToken cancellationToken);
}
