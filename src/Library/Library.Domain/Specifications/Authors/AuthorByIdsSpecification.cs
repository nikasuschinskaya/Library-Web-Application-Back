using Ardalis.Specification;
using Library.Domain.Entities;

namespace Library.Domain.Specifications.Authors;

public class AuthorByIdsSpecification : Specification<Author>
{
    public AuthorByIdsSpecification(IEnumerable<Guid> authorIds)
    {
        Query.Where(author => authorIds.Contains(author.Id)).AsNoTracking();
    }
}
