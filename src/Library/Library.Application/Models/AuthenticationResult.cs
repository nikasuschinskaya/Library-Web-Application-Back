namespace Library.Application.Models;

public record class AuthenticationResult(
    Guid Id,
    string Username,
    string Email,
    string Password,
    string Token);