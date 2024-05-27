namespace HumanResourceManagement.Domain.Repositories;

public interface IEmployeeAttendencesRepository
{
    IQueryable<EmployeeAttendance> GetAll();
    Task<List<EmployeeAttendance>> GetAllAsync(int month, int year);
    IQueryable<EmployeeAttendance> GetAll(int employeeProfileId);
    Task<EmployeeAttendance> GetAsync(int id);
    Task InsertAsync(EmployeeAttendance entity, CancellationToken cancellationToken);
    Task UpdateAsync(EmployeeAttendance entity, CancellationToken cancellationToken);
    Task DeleteAsync(EmployeeAttendance entity, CancellationToken cancellationToken);
}
