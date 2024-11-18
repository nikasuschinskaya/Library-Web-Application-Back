using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Authors;

public interface IAddAuthorUseCase : IUseCase
{
    Task ExecuteAsync(Author author, CancellationToken cancellationToken = default);
}
