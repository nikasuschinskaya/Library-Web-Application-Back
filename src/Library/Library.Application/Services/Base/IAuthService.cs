using Library.Application.Models;
using Library.Domain.Entities;

namespace Library.Application.Services.Base;

public interface IAuthService
{
    Task<User> RegisterAsync(string name, string email, string password, CancellationToken cancellationToken = default);
    Task<TokenResponse> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<TokenResponse> RefreshAccessTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
}