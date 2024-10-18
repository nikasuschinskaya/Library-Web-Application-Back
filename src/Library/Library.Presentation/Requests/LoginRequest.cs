namespace Library.Presentation.Requests;

public record class LoginRequest(
    string Email,
    string Password);
