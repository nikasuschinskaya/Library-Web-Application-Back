using Library.Application.Interfaces;
using Library.Domain.Entities.Base;

namespace Library.Infrastucture.Data.Initializers.Base
{
    public class BaseInitializer<T> : IInitializer<T> where T : BaseEntity
    {
        public IEnumerable<T> Entities { get; private set; }

        public BaseInitializer(IEnumerable<T> initialEntities) =>
            Entities = initialEntities;

        public void Initialize(IUnitOfWork unitOfWork)
        {
            var repository = unitOfWork.Repository<T>();

            foreach (var entity in Entities)
            {
                repository.Create(entity);
            }

            unitOfWork.CompleteAsync();
        }
    }
}
