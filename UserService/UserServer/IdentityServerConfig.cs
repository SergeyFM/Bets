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
                        new Secret("sectet".Sha256())
                    },
                    AllowedScopes = {"api1"}
                }
            };
        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Scopes = { "api1" },
                    ApiSecrets = { new Secret("ScopeSecret".Sha256()) }
                }
            };
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
    }
}