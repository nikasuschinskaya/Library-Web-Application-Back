using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Books;

public interface ISearchBooksByTitleUseCase : IUseCase
{
    Task<IEnumerable<Book>> ExecuteAsync(string title, CancellationToken cancellationToken = default);
}
