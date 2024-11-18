using Ardalis.Specification;
using Library.Domain.Entities;

namespace Library.Domain.Specifications.Genres;

public class AllGenresOrderedSpecification : Specification<Genre>
{
    public AllGenresOrderedSpecification()
    {
        Query.OrderBy(genre => genre.Name)
             .AsNoTracking();
    }
}
