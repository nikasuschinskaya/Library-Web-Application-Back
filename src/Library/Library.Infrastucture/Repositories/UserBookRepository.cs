using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastucture.Data;
using Library.Infrastucture.Repositories.Base;

namespace Library.Infrastucture.Repositories;

public class UserBookRepository : EntityFrameworkRepository<UserBook>, IUserBookRepository
{
    public UserBookRepository(LibraryDbContext context) : base(context)
    {
    }
}
