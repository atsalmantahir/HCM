namespace HumanResourceManagement.Application.Common.Models;

public class PaginatedList<T>
{
    public IList<T> Items { get; }
    public int? PageNumber { get; }
    public int? TotalPages { get; }
    public int TotalCount { get; }

    public PaginatedList(IList<T> items, int count, int? pageNumber, int? pageSize)
    {
        PageNumber = pageNumber.HasValue ? pageNumber.Value : 0;
        TotalPages = pageSize.HasValue ? (int)Math.Ceiling(count / (double)pageSize.Value) : 0;
        TotalCount = count;
        Items = items;
    }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int? pageNumber, int? pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.ToListAsync();
        if (pageNumber.HasValue && pageSize.HasValue)
        {
            items = await source.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value).ToListAsync();
        }

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}
