namespace HumanResourceManagement.Domain.Repositories;

public interface IEmployeeLoansRepository
{
    IQueryable<EmployeeLoan> GetAll();
    IQueryable<EmployeeLoan> GetAll(int employeeProfileId);
    Task<EmployeeLoan> GetAsync(int id);
    Task InsertAsync(EmployeeLoan entity, CancellationToken cancellationToken);
    Task UpdateAsync(EmployeeLoan entity, CancellationToken cancellationToken);
    Task DeleteAsync(EmployeeLoan entity, CancellationToken cancellationToken);
}
