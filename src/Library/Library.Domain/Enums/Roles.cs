using Library.Domain.Extentions;

namespace Library.Domain.Enums;

public enum Roles
{
    [StringValue("User")]
    User,
    [StringValue("Admin")]
    Admin
}
