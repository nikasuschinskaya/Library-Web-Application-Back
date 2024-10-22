using Library.Application.Interfaces.Common;
using Library.Domain.Entities.Base;
using Library.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastucture.Repositories;

public class EntityFrameworkRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly LibraryDbContext _context;
    private readonly DbSet<T> _dbSet;

    public EntityFrameworkRepository(LibraryDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public void Create(T entity) => _dbSet.Add(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);

    public IQueryable<T> GetAll() => _dbSet;

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _dbSet.Where(entity => entity.Id == id).FirstOrDefaultAsync(cancellationToken);

    public void Update(T entity) => _dbSet.Update(entity);
}
