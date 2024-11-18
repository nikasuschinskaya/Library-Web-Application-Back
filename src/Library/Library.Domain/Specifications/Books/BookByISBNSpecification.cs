using Ardalis.Specification;
using Library.Domain.Entities;

namespace Library.Domain.Specifications.Books;

public class BookByISBNSpecification : Specification<Book>
{
    public BookByISBNSpecification(string iSBN)
    {
        Query.Where(book => book.ISBN == iSBN)
             .AsNoTracking();
    }
}
