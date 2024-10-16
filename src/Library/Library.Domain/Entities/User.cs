using Library.Domain.Entities.Base;
using Library.Domain.Enums;

namespace Library.Domain.Entities
{
    public class User : NamedEntity
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public virtual Role Role { get; set; }
        public virtual List<Book> Books { get; set; }

        public User() { }

        public User(string name, string email, string passwordHash)
        : this(Guid.NewGuid(), name, email, passwordHash, new Role(nameof(Roles.User)), []) { }

        public User(Guid id, string name, string email, string passwordHash, Role role, List<Book> books)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            Books = books;
        }
    }
}
