namespace Library.Presentation.Responses;

public record class AuthTokensResponse(
    string AccessToken, 
    string RefreshToken);
