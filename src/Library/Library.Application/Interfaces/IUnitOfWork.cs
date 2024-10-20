using Library.Domain.Entities.Base;

namespace Library.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : BaseEntity;
    IRefreshTokenRepository RefreshTokenRepository {  get; }
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}
