using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Authors;

public interface IGetAllAuthorsUseCase : IUseCase
{
    Task<IEnumerable<Author>> ExecuteAsync(CancellationToken cancellationToken = default);
}