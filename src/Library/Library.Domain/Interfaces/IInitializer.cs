using Library.Domain.Entities.Base;

namespace Library.Domain.Interfaces;

public interface IInitializer<T> where T : IEntity
{
    //IEnumerable<T> Entities { get; }
    //Task InitializeAsync(IUnitOfWork unitOfWork);
    Task InitializeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}
