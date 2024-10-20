using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Library.Infrastucture.IdentityServer;

public static class IdentityServerConfig
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("api1", "My API")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AccessTokenLifetime = 900, // 15 минут для access token
                AllowOfflineAccess = true, // включаем refresh token
                RefreshTokenUsage = TokenUsage.ReUse,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                SlidingRefreshTokenLifetime = 1296000, // 15 дней
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "api1", IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile }
            }
        };
}

