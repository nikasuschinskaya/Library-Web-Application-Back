using Library.Domain.Entities;

namespace Library.Presentation.Responses;

public record class UserResponse(
    string Name,
    string Email,
    string Role,
    IReadOnlyCollection<UserBook> UserBooks);
