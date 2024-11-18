using Ardalis.Specification;
using Library.Domain.Entities;

namespace Library.Domain.Specifications.Books;

public class BooksByAuthorSpecification : Specification<Book>
{
    public BooksByAuthorSpecification(Guid authorId)
    {
        Query.Include(b => b.Authors)
             .Where(book => book.Authors.Any(author => author.Id == authorId))
             .AsNoTracking();
    }
}
