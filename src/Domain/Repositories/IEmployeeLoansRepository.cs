namespace HumanResourceManagement.Domain.Repositories;

public interface IEmployeeLoansRepository
{
    IQueryable<EmployeeLoan> GetAll();
    Task<EmployeeLoan> GetAsync(int id);
    Task<EmployeeLoan> GetAsync(string externalIdentifier);
    Task InsertAsync(EmployeeLoan entity, CancellationToken cancellationToken);
    Task UpdateAsync(EmployeeLoan entity, CancellationToken cancellationToken);
    Task DeleteAsync(EmployeeLoan entity, CancellationToken cancellationToken);
}
