using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastucture.Data;
using Library.Infrastucture.Repositories.Base;

namespace Library.Infrastucture.Repositories;

public class RoleRepository : EntityFrameworkRepository<Role>, IRoleRepository
{
    public RoleRepository(LibraryDbContext context) : base(context)
    {
    }
}
