using Duende.IdentityServer.Models;

namespace UserServer
{
    internal class IdentityServerConfig
    {
        public static ICollection<Client> Clients => new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"api1"}
                },
                new Client
                {
                    ClientId = "api-gateway",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("your_secret".Sha256())
                    },
                    AllowedScopes = { "api-gateway" }
                }
            };
        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "api1",
                    Scopes = { "api1" },
                    ApiSecrets = { new Secret("ScopeSecret".Sha256()) }
                },
                new ApiResource
                {
                    Name = "api-gateway",
                    Scopes = { "api-gateway" },
                    ApiSecrets = { new Secret("ScopeApiGateway".Sha256())}
                }
            };
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api1"),
                new ApiScope("api-gateway")
            };
    }
}