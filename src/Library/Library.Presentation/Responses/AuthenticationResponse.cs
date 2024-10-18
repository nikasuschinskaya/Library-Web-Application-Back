namespace Library.Presentation.Responses;

public record class AuthenticationResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);
