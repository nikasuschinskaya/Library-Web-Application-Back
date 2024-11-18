using Library.Domain.Models;

namespace Library.Domain.Interfaces.Auth;

public interface IAuthService
{
    Task<AuthTokens> RegisterAsync(string name, string email, string password, CancellationToken cancellationToken = default);
    Task<AuthTokens> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<AuthTokens> RefreshAccessTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default);
    Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
}