namespace Library.Domain.Entities.Base;

public class BaseEntity : IEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
}
