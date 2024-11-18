using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Users;

public interface IUpdateUserUseCase : IUseCase
{
    Task ExecuteAsync(Guid userId, User user, CancellationToken cancellationToken = default);
}
