using Library.Application.Interfaces.Common;
using Library.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Infrastucture.Data.Initializers.Base;

public class BaseInitializer<T> : IInitializer<T> where T : BaseEntity
{
    public IEnumerable<T> Entities { get; private set; }

    public BaseInitializer(IEnumerable<T> initialEntities) =>
        Entities = initialEntities;

    public async Task InitializeAsync(IUnitOfWork unitOfWork)
    {
        var repository = unitOfWork.Repository<T>();

        var entitiesExist = await repository.GetAll().AnyAsync();

        if (!entitiesExist)
        {
            foreach (var entity in Entities)
            {
                repository.Create(entity);
            }

            await unitOfWork.CompleteAsync();
        }
    }
}
