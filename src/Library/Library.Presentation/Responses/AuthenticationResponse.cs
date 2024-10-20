namespace Library.Presentation.Responses;

public record class AuthenticationResponse(
    Guid Id,
    string Username,
    string Email,
    string Password,
    string Token);
