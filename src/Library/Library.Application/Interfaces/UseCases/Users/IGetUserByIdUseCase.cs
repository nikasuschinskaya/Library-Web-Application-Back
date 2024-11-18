using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Users;

public interface IGetUserByIdUseCase : IUseCase
{
    Task<User?> ExecuteAsync(Guid userId, CancellationToken cancellationToken = default);
}