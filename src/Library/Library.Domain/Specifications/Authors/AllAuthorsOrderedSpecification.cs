using Ardalis.Specification;
using Library.Domain.Entities;

namespace Library.Domain.Specifications.Authors;

public class AllAuthorsOrderedSpecification : Specification<Author>
{
    public AllAuthorsOrderedSpecification()
    {
        Query.OrderBy(author => author.Name)
             .ThenBy(author => author.Surname)
             .AsNoTracking();
    }
}
