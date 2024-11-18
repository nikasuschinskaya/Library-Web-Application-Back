namespace Library.Application.Interfaces.UseCases.Books;

public interface IDeleteBookUseCase : IUseCase
{
    Task ExecuteAsync(Guid bookId, CancellationToken cancellationToken = default);
}
