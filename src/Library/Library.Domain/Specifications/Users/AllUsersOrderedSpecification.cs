using Ardalis.Specification;
using Library.Domain.Entities;

namespace Library.Domain.Specifications.Users;

public class AllUsersOrderedSpecification : Specification<User>
{
    public AllUsersOrderedSpecification()
    {
        Query.Include(u => u.UserBooks)
             .Include(r => r.Role)
             .OrderBy(user => user.Name)
             .AsTracking();
    }
}
