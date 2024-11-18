using Ardalis.Specification;
using Library.Domain.Entities;

namespace Library.Domain.Specifications.Books;

public class BooksByGenreAndAuthorSpecification : Specification<Book>
{
    public BooksByGenreAndAuthorSpecification(string? genre, string? authorName)
    {
        Query.Include(b => b.Genre)
             .Include(b => b.Authors);

        if (!string.IsNullOrWhiteSpace(genre))
        {
            var genreLower = genre.ToLower();
            Query.Where(book => book.Genre.Name.ToLower() == genreLower).AsNoTracking();
        }

        if (!string.IsNullOrWhiteSpace(authorName))
        {
            var authorNameLower = authorName.ToLower();
            Query.Where(book => book.Authors.Any(a => a.Surname.ToLower().Contains(authorNameLower))).AsNoTracking();
        }
    }
}
