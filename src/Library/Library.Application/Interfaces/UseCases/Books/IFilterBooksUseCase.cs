using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Books;

public interface IFilterBooksUseCase : IUseCase
{
    Task<IEnumerable<Book>> ExecuteAsync(string? genre, string? authorName, CancellationToken cancellationToken = default);
}
