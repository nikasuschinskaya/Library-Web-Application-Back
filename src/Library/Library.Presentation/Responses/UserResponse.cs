using Library.Domain.Entities;

namespace Library.Presentation.Responses;

public record class UserResponse(
    Guid Id,
    string Name,
    string Email,
    Role Role,
    IReadOnlyCollection<UserBook> UserBooks);
