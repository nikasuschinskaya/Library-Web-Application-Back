using Library.Domain.Interfaces;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastucture.Repositories;

namespace Library.Infrastucture.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryDbContext _context;
    private bool _disposed;

    public UnitOfWork(LibraryDbContext context) => _context = context;


    private IAuthorRepository? _authors;
    public IAuthorRepository Authors => _authors ??= new AuthorRepository(_context);


    private IBookRepository? _books;
    public IBookRepository Books => _books ??= new BookRepository(_context);


    private IGenreRepository? _genres;
    public IGenreRepository Genres => _genres ??= new GenreRepository(_context);


    private IRefreshTokenRepository? _refreshTokens;
    public IRefreshTokenRepository RefreshTokens => _refreshTokens ??= new RefreshTokenRepository(_context);


    private IRoleRepository? _roles;
    public IRoleRepository Roles => _roles ??= new RoleRepository(_context);


    private IUserBookRepository? _userBooks;
    public IUserBookRepository UserBooks => _userBooks ??= new UserBookRepository(_context);


    private IUserRepository? _users;
    public IUserRepository Users => _users ??= new UserRepository(_context);


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
