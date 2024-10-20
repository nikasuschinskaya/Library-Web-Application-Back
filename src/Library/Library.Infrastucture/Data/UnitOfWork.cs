using Library.Application.Interfaces;
using Library.Domain.Entities.Base;
using Library.Infrastucture.Repositories;

namespace Library.Infrastucture.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryDbContext _context;
    private readonly Dictionary<string, object> _repositories;

    public UnitOfWork(LibraryDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<string, object>();
    }

    public IRepository<T> Repository<T>() where T : BaseEntity
    {
        var type = typeof(T).Name;

        if (!_repositories.TryGetValue(type, out object? value))
        {
            var repositoryInstance = new EntityFrameworkRepository<T>(_context);
            value = repositoryInstance;
            _repositories.Add(type, value);
        }

        return (IRepository<T>)value;
    }

    public IRefreshTokenRepository RefreshTokenRepository => new RefreshTokenRepository(_context);

    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);

    public void Dispose() => _context.Dispose();
}
