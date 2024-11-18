namespace Library.Application.Interfaces.UseCases.Books;

public interface ITakeBookUseCase : IUseCase
{
    Task ExecuteAsync(Guid bookId, Guid userId, CancellationToken cancellationToken = default);
}
