using Ardalis.Specification;
using Library.Domain.Entities.Base;

namespace Library.Domain.Interfaces.Repositories;

public interface IRepository<T> where T : IEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<T?> GetBySpecAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    //Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    //IQueryable<T> GetAll();
    //void Create(T entity);
    //void Update(T entity);
    //void Delete(T entity);
}