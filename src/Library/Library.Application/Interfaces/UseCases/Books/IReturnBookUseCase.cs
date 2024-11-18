namespace Library.Application.Interfaces.UseCases.Books;

public interface IReturnBookUseCase : IUseCase
{
    Task ExecuteAsync(Guid bookId, Guid userId, CancellationToken cancellationToken = default);
}
