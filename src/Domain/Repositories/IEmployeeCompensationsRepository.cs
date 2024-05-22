namespace HumanResourceManagement.Domain.Repositories;

public interface IEmployeeCompensationsRepository
{
    IQueryable<EmployeeCompensation> GetAll();
    Task<EmployeeCompensation> GetAsync(int id);
    Task<EmployeeCompensation> GetAsync(string externalIdentifier);
    Task<EmployeeCompensation> GetByEmployeeProfileAsync(string employeeProfileExternalIdentifier);
    Task InsertAsync(EmployeeCompensation entity, CancellationToken cancellationToken);
    Task UpdateAsync(EmployeeCompensation entity, CancellationToken cancellationToken);
    Task DeleteAsync(EmployeeCompensation entity, CancellationToken cancellationToken);
}
