namespace Library.Presentation.Authentication;

public record class AuthenticationResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);
