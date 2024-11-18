using Ardalis.Specification;
using Library.Domain.Entities;

namespace Library.Domain.Specifications.Books;

public class PagedBooksSpecification : Specification<Book>
{
    public PagedBooksSpecification()
    {
        Query.Include(book => book.Authors)
             .OrderBy(book => book.Name)
             .AsNoTracking();
    }
}
