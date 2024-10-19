namespace Library.Domain.Exceptions;

public class EntityDublicateException : Exception
{
    public EntityDublicateException() : base("Entity is already exists in database") { }
}
