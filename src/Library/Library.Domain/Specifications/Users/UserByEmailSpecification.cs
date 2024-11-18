using Ardalis.Specification;
using Library.Domain.Entities;

namespace Library.Domain.Specifications.Users;

public class UserByEmailSpecification : Specification<User>
{
    public UserByEmailSpecification(string email)
    {
        Query.Where(user => user.Email == email)
             .AsNoTracking();
    }
}
