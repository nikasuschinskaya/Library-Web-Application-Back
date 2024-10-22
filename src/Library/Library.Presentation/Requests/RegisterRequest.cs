namespace Library.Presentation.Requests;

public record class RegisterRequest(
    string Name,
    string Email,
    string Password);
