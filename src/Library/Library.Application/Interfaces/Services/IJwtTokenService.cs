namespace Library.Application.Interfaces.Services;

public interface IJwtTokenService
{
    string GenerateAccessToken(string userId);
    bool ValidateToken(string token);
}