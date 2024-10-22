using Library.Domain.Entities;

namespace Library.Application.Interfaces.Services;

public interface IAuthorService
{
    Task<IEnumerable<Author>> GetAllAuthorsAsync(CancellationToken cancellationToken = default);
    Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAuthorAsync(Author author, CancellationToken cancellationToken = default);
    Task UpdateAuthorAsync(Author author, CancellationToken cancellationToken = default);
    Task DeleteAuthorAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId, CancellationToken cancellationToken = default); //под вопросом - тут оставить или в бук
}
