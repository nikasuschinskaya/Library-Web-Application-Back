using Library.Domain.Entities.Base;

namespace Library.Application.Interfaces;

public interface IRepository<T> /*where T : IEntity*/
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    IEnumerable<T> GetAll();
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}   