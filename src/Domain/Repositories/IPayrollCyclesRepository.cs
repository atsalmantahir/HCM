namespace HumanResourceManagement.Domain.Repositories;

public interface IPayrollCyclesRepository
{
    IQueryable<PayrollCycle> GetAll();
    Task<PayrollCycle> GetAsync(int id);
    Task InsertAsync(PayrollCycle entity, CancellationToken cancellationToken);
    Task InsertListAsync(IList<PayrollCycle> payrolls, CancellationToken cancellationToken);
    Task UpdateAsync(PayrollCycle entity, CancellationToken cancellationToken);
    Task DeleteAsync(PayrollCycle entity, CancellationToken cancellationToken);
}
