namespace Library.Presentation.Authentication;

public record class RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);
