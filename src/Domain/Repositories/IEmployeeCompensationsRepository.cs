namespace HumanResourceManagement.Domain.Repositories;

public interface IEmployeeCompensationsRepository
{
    IQueryable<EmployeeCompensation> GetAll();
    Task<EmployeeCompensation> GetAsync(int id);
    Task InsertAsync(EmployeeCompensation entity, CancellationToken cancellationToken);
    Task UpdateAsync(EmployeeCompensation entity, CancellationToken cancellationToken);
    Task DeleteAsync(EmployeeCompensation entity, CancellationToken cancellationToken);
    Task<EmployeeCompensation> GetByEmployeeProfileAsync(int employeeProfileId);
}
