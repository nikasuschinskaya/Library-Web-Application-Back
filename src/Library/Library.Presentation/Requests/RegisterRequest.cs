namespace Library.Presentation.Requests;

public record class RegisterRequest(
    string Username,
    string Email,
    string Password);
