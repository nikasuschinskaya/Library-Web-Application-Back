using Library.Domain.Entities;

namespace Library.Presentation.Requests;

public record class UserRequest(
    string Name,
    string Email,
    string Password,
    Role Role);
