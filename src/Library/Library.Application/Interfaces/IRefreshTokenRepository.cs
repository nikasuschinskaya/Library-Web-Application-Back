using Library.Domain.Entities;

namespace Library.Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
    Task RemoveAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
}

