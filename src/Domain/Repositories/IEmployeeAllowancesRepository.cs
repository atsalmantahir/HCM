namespace HumanResourceManagement.Domain.Repositories;

public interface IEmployeeAllowancesRepository
{
    IQueryable<EmployeeAllowance> GetAll();
    Task<EmployeeAllowance> GetAsync(int id);
    Task InsertAsync(EmployeeAllowance entity, CancellationToken cancellationToken);
    Task UpdateAsync(EmployeeAllowance entity, CancellationToken cancellationToken);
    Task DeleteAsync(EmployeeAllowance entity, CancellationToken cancellationToken);
}
