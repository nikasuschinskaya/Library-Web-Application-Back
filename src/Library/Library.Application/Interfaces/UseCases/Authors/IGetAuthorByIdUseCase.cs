using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Authors;

public interface IGetAuthorByIdUseCase : IUseCase
{
    Task<Author?> ExecuteAsync(Guid authorId, CancellationToken cancellationToken = default);
}
