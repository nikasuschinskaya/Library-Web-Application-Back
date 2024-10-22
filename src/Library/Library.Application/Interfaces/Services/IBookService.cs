using Library.Domain.Entities;

namespace Library.Application.Interfaces.Services;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken = default);
    Task<Book?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Book?> GetBookByISBNAsync(string iSBN, CancellationToken cancellationToken = default);
    Task AddBookAsync(Book book, CancellationToken cancellationToken = default);
    Task UpdateBookAsync(Book book, CancellationToken cancellationToken = default);
    Task DeleteBookAsync(Guid id, CancellationToken cancellationToken = default);
    Task TakeBookAsync(Guid bookId, Guid userId, CancellationToken cancellationToken = default);
    Task ReturnBookAsync(Guid bookId, Guid userId, CancellationToken cancellationToken = default);
    Task AddBookImageAsync(Guid id, string imageURL, CancellationToken cancellationToken = default);
}