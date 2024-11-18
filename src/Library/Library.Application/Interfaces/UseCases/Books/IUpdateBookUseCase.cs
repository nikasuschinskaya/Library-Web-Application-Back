using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Books;

public interface IUpdateBookUseCase : IUseCase
{
    Task ExecuteAsync(Guid bookId, Book updatedBook, CancellationToken cancellationToken = default);
}
