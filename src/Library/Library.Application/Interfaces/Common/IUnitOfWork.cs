using Library.Domain.Entities.Base;

namespace Library.Application.Interfaces.Common;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : BaseEntity;
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}
