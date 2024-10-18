namespace Library.Domain.Exceptions;

public class EntityNotFoundException(string message) : Exception(message)
{
    public EntityNotFoundException() : this("Entity not found") { }

    public EntityNotFoundException(Guid id) : this($"Entity with ID: {id} not found") => Id = id;

    public EntityNotFoundException(Guid id, Type entityType) : this($"Entity {entityType.Name} with ID: {id} not found")
    {
        Id = id;
        EntityType = entityType;
    }

    public Guid Id { get; init; }

    public Type EntityType { get; init; }
}
