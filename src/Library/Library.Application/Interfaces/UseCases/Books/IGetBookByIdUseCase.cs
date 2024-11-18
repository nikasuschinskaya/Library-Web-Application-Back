using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Books;

public interface IGetBookByIdUseCase : IUseCase
{
    Task<Book?> ExecuteAsync(Guid bookId, CancellationToken cancellationToken = default);
}
