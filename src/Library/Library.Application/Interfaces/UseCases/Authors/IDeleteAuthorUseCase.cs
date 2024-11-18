namespace Library.Application.Interfaces.UseCases.Authors;

public interface IDeleteAuthorUseCase : IUseCase
{
    Task ExecuteAsync(Guid authorId, CancellationToken cancellationToken = default);
}
