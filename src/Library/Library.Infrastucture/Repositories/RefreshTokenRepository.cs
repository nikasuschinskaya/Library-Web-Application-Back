using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastucture.Repositories;

public class RefreshTokenRepository : EntityFrameworkRepository<RefreshToken>, IRefreshTokenRepository
{
    private readonly LibraryDbContext _context;

    public RefreshTokenRepository(LibraryDbContext context) : base(context) => _context = context;

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await _context.Set<RefreshToken>().FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
    }

    public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        await _context.Set<RefreshToken>().AddAsync(refreshToken, cancellationToken);
    }

    public async Task RemoveAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        _context.Set<RefreshToken>().Remove(refreshToken);
    }
}

