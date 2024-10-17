using Library.Domain.Entities.Base;

namespace Library.Application.Interfaces
{
    public interface IInitializer<T> where T : IEntity
    {
        IEnumerable<T> Entities { get; }

        void Initialize(IUnitOfWork unitOfWork);
    }
}
