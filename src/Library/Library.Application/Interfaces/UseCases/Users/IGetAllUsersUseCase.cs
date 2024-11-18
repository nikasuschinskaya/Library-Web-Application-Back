using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Users;

public interface IGetAllUsersUseCase : IUseCase
{
    Task<IEnumerable<User>> ExecuteAsync(CancellationToken cancellationToken = default);
}
