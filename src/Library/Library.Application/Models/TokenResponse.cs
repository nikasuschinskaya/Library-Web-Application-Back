namespace Library.Application.Models;

public class TokenResponse
{
    public string AccessToken { get; }
    public string RefreshToken { get; }

    public TokenResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}

