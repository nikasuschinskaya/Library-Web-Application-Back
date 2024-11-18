using Ardalis.Specification;
using Library.Domain.Entities;

namespace Library.Domain.Specifications.Books;

public class BooksByTitleSpecification : Specification<Book>
{
    public BooksByTitleSpecification(string title)
    {
        Query.Include(book => book.Authors)
             .Where(book => book.Name.Contains(title))
             .AsNoTracking();
    }
}
