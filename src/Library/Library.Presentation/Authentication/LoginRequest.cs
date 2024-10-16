namespace Library.Presentation.Authentication;

public record class LoginRequest(
    string Email,
    string Password);
