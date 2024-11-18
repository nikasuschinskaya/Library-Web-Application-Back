using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Genres;

public interface IGetAllGenresUseCase : IUseCase
{
    Task<IEnumerable<Genre>> ExecuteAsync(CancellationToken cancellationToken = default);
}
