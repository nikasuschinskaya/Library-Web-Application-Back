using Ardalis.Specification;

namespace Library.Domain.Specifications;

public class EmptySpecification<T> : Specification<T>
{
    public EmptySpecification()
    {
        Query.Where(_ => true);
    }
}
