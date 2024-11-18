namespace Library.Domain.Interfaces.Auth;

public interface IJwtTokenService
{
    string GenerateAccessToken(string userId);
    bool ValidateToken(string token);
}