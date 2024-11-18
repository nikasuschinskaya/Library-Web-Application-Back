using Ardalis.Specification;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Models;
using Library.Infrastucture.Data;
using Library.Infrastucture.Repositories.Base;

namespace Library.Infrastucture.Repositories;

public class BookRepository : EntityFrameworkRepository<Book>, IBookRepository
{
    public BookRepository(LibraryDbContext context) : base(context)
    {

    }

    public async Task<PagedList<Book>> ListPagedAsync(ISpecification<Book> spec, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = ApplySpecification(spec);
        return await PagedList<Book>.CreateAsync(query, pageNumber, pageSize, cancellationToken);
    }
}
