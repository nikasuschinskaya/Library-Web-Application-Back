using Library.Domain.Interfaces.Repositories;

namespace Library.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAuthorRepository Authors { get; }
    IBookRepository Books { get; }
    IGenreRepository Genres { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    IRoleRepository Roles { get; }
    IUserBookRepository UserBooks { get; }
    IUserRepository Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
