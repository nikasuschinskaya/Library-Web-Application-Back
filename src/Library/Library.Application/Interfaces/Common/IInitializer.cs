using Library.Domain.Entities.Base;

namespace Library.Application.Interfaces.Common;

public interface IInitializer<T> where T : IEntity
{
    IEnumerable<T> Entities { get; }
    Task InitializeAsync(IUnitOfWork unitOfWork);
}
