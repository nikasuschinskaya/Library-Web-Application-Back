using Library.Domain.Entities.Base;

namespace Library.Domain.Interfaces;

public interface IInitializer<T> where T : IEntity
{
    Task InitializeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}
