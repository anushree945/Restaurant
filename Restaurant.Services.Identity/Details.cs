using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Restaurant.Services.Identity
{
    public static class Details
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources = new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };
        public static IEnumerable<ApiScope> ApiScopes = new List<ApiScope>()
        {
            new ApiScope("Restaurant", "Restaurant Server"),
            new ApiScope("read", "Read your Data"),
            new ApiScope("write", "Write your Data"),
            new ApiScope("delete", "Delete your Data")
        };
        public static IEnumerable<Client> Clients = new List<Client>()
        {
            new Client()
            {
                ClientId = "client",
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"read", "write", "profile"}
            },
            new Client()
            {
                ClientId = "restaurant",
                ClientSecrets = {new Secret("secret".Sha256())},
                RedirectUris = { "https://localhost:44462/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:44462/signout-callback-oidc" },
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = new List<string>()
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile,
                    "Restaurant"
                }
            }
        };
    }
}
