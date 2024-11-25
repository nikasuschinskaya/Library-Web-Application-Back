using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastucture.Data;
using Library.Infrastucture.Repositories.Base;

namespace Library.Infrastucture.Repositories;

public class AuthorRepository : EntityFrameworkRepository<Author>, IAuthorRepository
{
    public AuthorRepository(LibraryDbContext context) : base(context)
    {
    }
}