namespace HumanResourceManagement.Domain.Repositories;

public interface IEmployeeAttendencesRepository
{
    IQueryable<EmployeeAttendance> GetAll();
    Task<EmployeeAttendance> GetAsync(int id);
    Task<EmployeeAttendance> GetAsync(string externalIdentifier);
    Task InsertAsync(EmployeeAttendance entity, CancellationToken cancellationToken);
    Task UpdateAsync(EmployeeAttendance entity, CancellationToken cancellationToken);
    Task DeleteAsync(EmployeeAttendance entity, CancellationToken cancellationToken);
}
