using Library.Domain.Entities.Base;
using Library.Domain.Interfaces;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Specifications;

namespace Library.Infrastucture.Data.Initializers.Base;

public class BaseInitializer<T> : IInitializer<T> where T : class, IEntity
{
    private readonly IRepository<T> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public BaseInitializer(IRepository<T> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task InitializeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        var hasAnyRecords = (await _repository.ListAsync(new EmptySpecification<T>(), cancellationToken)).Any();
        if (hasAnyRecords) return; 
       
        foreach (var entity in entities)
            _repository.Create(entity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    //public async Task InitializeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    //{
    //    foreach (var entity in entities)
    //    {
    //        var existingEntity = await _repository.GetByIdAsync(entity.Id, cancellationToken);
    //        if (existingEntity == null)
    //        {
    //            _repository.Create(entity);
    //        }
    //    }

    //    await _unitOfWork.SaveChangesAsync(cancellationToken);
    //}
}

//public class BaseInitializer<T> : IInitializer<T> where T : class, IEntity
//{
//    public IEnumerable<T> Entities { get; private set; }

//    public BaseInitializer(IEnumerable<T> initialEntities) =>
//        Entities = initialEntities;

//    public async Task InitializeAsync(IUnitOfWork unitOfWork)
//    {
//        var repository = unitOfWork.Repository<T>();

//        var entitiesExist = await repository.GetAll().AnyAsync();

//        if (!entitiesExist)
//        {
//            foreach (var entity in Entities)
//            {
//                repository.Create(entity);
//            }

//            await unitOfWork.SaveChangesAsync();
//        }
//    }
//}
