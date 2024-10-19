using Library.Domain.Entities;

namespace Library.Application.Services.Base;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<User> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task Update(User user, CancellationToken cancellationToken = default);
    Task NotifyUserAboutBookExpirationAsync(Guid userId, Guid bookId, CancellationToken cancellationToken);
    Task<User> RegisterAsync(string name, string email, string password, CancellationToken cancellationToken = default);
    Task<User?> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
}