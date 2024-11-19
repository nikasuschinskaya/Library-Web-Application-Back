using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Library.Domain.Entities.Base;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastucture.Repositories.Base;

public class EntityFrameworkRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly LibraryDbContext _context;
    private readonly DbSet<T> _dbSet;

    public EntityFrameworkRepository(LibraryDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public void Create(T entity) => _dbSet.Add(entity);
    public void Update(T entity) => _dbSet.Update(entity);
    public void Delete(T entity) => _dbSet.Remove(entity);
    public void Attach(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
            _dbSet.Attach(entity);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(entity => entity.Id == id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> GetBySpecAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec).ToListAsync(cancellationToken);
    }

    protected IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator.Default.GetQuery(_dbSet.AsQueryable(), spec);
    }
}