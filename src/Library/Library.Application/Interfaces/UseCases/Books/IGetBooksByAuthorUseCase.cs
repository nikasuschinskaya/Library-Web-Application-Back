using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Books;

public interface IGetBooksByAuthorUseCase : IUseCase
{
    Task<IEnumerable<Book>> ExecuteAsync(Guid authorId, CancellationToken cancellationToken = default);
}
