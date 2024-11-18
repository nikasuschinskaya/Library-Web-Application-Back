using Ardalis.Specification;
using Library.Domain.Entities;
using Library.Domain.Enums;

namespace Library.Domain.Specifications.Books;

public class ActiveUserBookSpecification : Specification<UserBook>
{
    public ActiveUserBookSpecification(Guid userId, Guid bookId)
    {
        Query.Where(userBook =>
               userBook.UserId == userId &&
               userBook.BookId == bookId &&
               userBook.Status == UserBookStatus.Taken);
    }
}
