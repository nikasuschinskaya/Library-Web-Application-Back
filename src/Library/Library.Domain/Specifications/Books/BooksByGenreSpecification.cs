using Ardalis.Specification;
using Library.Domain.Entities;

namespace Library.Domain.Specifications.Books;

public class BooksByGenreSpecification : Specification<Book>
{
    public BooksByGenreSpecification(string genre)
    {
        Query.Include(x => x.Genre)
            .Where(book => book.Genre.Name == genre)
            .AsNoTracking();
    }
}
