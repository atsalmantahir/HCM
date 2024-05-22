namespace HumanResourceManagement.Domain.Repositories;

public interface IEmployeeEducationsRepository
{
    Task<IList<EmployeeEducation>> GetAllAsync();
    Task<EmployeeEducation> GetAsync(int id);
    Task<EmployeeEducation> GetAsync(string externalIdentifier);
    Task InsertAsync(EmployeeEducation entity, CancellationToken cancellationToken);
    Task UpdateAsync(EmployeeEducation entity, CancellationToken cancellationToken);
    Task DeleteAsync(EmployeeEducation entity, CancellationToken cancellationToken);
}
