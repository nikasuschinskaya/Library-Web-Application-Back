using Ardalis.Specification;
using Library.Domain.Entities;

namespace Library.Domain.Specifications.Roles;

public class RoleByNameSpecification : Specification<Role>
{
    public RoleByNameSpecification(string roleName)
    {
        Query.Where(role => role.Name == roleName);
    }
}
