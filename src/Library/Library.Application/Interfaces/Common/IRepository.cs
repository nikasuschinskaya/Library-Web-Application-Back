using Library.Domain.Entities.Base;
using System.Linq.Expressions;

namespace Library.Application.Interfaces.Common;

public interface IRepository<T> where T : IEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    IQueryable<T> GetAll();
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}