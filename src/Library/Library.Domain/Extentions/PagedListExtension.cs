using Library.Domain.Models;

namespace Library.Domain.Extentions;

public static class PagedListExtension
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> values, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await PagedList<T>.CreateAsync(values, pageNumber, pageSize, cancellationToken);
    }
}
