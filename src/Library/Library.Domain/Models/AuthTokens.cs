namespace Library.Domain.Models;

public class AuthTokens
{
    public string AccessToken { get; }
    public string RefreshToken { get; }

    public AuthTokens(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}