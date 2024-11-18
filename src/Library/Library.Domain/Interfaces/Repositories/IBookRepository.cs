using Ardalis.Specification;
using Library.Domain.Entities;
using Library.Domain.Models;

namespace Library.Domain.Interfaces.Repositories;

public interface IBookRepository : IRepository<Book>
{
    Task<PagedList<Book>> ListPagedAsync(ISpecification<Book> spec, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
