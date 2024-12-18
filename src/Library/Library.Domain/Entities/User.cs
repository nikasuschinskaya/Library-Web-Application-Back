﻿using Library.Domain.Entities.Base;

namespace Library.Domain.Entities;

public class User : NamedEntity
{
    public string Email { get; set; }
    public string Password { get; set; }
    public virtual Role Role { get; set; }
    public virtual List<UserBook> UserBooks { get; set; }

    public User() { }

    public User(string name, string email, string password, Role role)
    : this(Guid.NewGuid(), name, email, password, role, []) { }

    public User(Guid id, string name, string email, string password, Role role, List<UserBook> userBooks)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        UserBooks = userBooks;
    }
}
