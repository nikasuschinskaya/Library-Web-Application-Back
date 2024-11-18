using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Users;

public interface IGetUserByEmailUseCase : IUseCase
{
    Task<User?> ExecuteAsync(string email, CancellationToken cancellationToken = default);
}
