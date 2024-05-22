namespace HumanResourceManagement.Application.Common.Models;

public record PaginatedQuery
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }

}
