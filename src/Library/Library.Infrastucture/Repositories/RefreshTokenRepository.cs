using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastucture.Data;
using Library.Infrastucture.Repositories.Base;

namespace Library.Infrastucture.Repositories;

public class RefreshTokenRepository : EntityFrameworkRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(LibraryDbContext context) : base(context)
    {
    }
}
