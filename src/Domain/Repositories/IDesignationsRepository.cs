namespace HumanResourceManagement.Domain.Repositories;

public interface IDesignationsRepository
{
    IQueryable<Designation> GetAll(int departmentId);
    Task<Designation> GetAsync(int Id);
    Task InsertAsync(Designation entity, CancellationToken cancellationToken);
    Task UpdateAsync(Designation entity, CancellationToken cancellationToken);
    Task DeleteAsync(Designation entity, CancellationToken cancellationToken);
}
