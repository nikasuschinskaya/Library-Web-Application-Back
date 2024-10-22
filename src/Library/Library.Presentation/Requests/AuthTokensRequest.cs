namespace Library.Presentation.Requests;

public record class AuthTokensRequest(
    string AccessToken, 
    string RefreshToken);
