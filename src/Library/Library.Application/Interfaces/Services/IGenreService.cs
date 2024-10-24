using Library.Domain.Entities;

namespace Library.Application.Interfaces.Services;

public interface IGenreService
{
    Task<IEnumerable<Genre>> GetAllGenresAsync(CancellationToken cancellationToken = default);
    Task<Genre?> GetGenreByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
