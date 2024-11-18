using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Genres;

public interface IGetGenreByIdUseCase : IUseCase
{
    Task<Genre?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}
