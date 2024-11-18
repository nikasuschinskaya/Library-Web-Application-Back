using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastucture.Data;
using Library.Infrastucture.Repositories.Base;

namespace Library.Infrastucture.Repositories;

public class GenreRepository : EntityFrameworkRepository<Genre>, IGenreRepository
{
    public GenreRepository(LibraryDbContext context) : base(context)
    {
    }
}
