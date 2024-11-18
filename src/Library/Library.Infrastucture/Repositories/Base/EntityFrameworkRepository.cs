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

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([id], cancellationToken);
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
    //public void Create(T entity) => _dbSet.Add(entity);

    //public void Delete(T entity) => _dbSet.Remove(entity);

    //public IQueryable<T> GetAll() => _dbSet;

    //public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
    //    await _dbSet.Where(entity => entity.Id == id).FirstOrDefaultAsync(cancellationToken);

    //public void Update(T entity) => _dbSet.Update(entity);
}
