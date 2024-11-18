using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Books;

public interface IAddBookUseCase : IUseCase
{
    Task ExecuteAsync(Book book, IEnumerable<Guid> authorIds, CancellationToken cancellationToken = default);
}
