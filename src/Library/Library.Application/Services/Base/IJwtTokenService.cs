namespace Library.Application.Services.Base;

public interface IJwtTokenService
{
    string GenerateAccessToken(string userId);
    bool ValidateToken(string token);
}