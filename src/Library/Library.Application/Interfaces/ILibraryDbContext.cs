using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Interfaces;

public interface ILibraryDbContext
{
    DbSet<Author> Authors { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<Book> Books { get; set; }
    DbSet<UserBook> UserBooks { get; set; }
    DbSet<RefreshToken> RefreshTokens { get; set; }
}
