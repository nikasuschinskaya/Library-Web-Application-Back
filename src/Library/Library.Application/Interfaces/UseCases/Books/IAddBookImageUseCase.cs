namespace Library.Application.Interfaces.UseCases.Books;

public interface IAddBookImageUseCase : IUseCase
{
    Task ExecuteAsync(Guid bookId, string imageURL, CancellationToken cancellationToken = default);
}
