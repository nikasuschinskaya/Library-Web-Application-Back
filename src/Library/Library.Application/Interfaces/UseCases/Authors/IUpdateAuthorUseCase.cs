using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Authors;

public interface IUpdateAuthorUseCase : IUseCase
{
    Task ExecuteAsync(Guid authorId, Author updatedAuthor, CancellationToken cancellationToken = default);
}
