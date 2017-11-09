using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Auth.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client()
                {
                    ClientId ="web-api",
                    ClientName = "Gym organizer web api",
                    AllowedGrantTypes = GrantTypes.Code,
                    //ClientSecrets = new List<Secret>(){new Secret("web-api secret") },
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api.fullAccess"
                    },
                    AllowedCorsOrigins = { "http://localhost:5002" }
                },
                new Client
                {
                    ClientId = "angular-client",
                    ClientName = "Gym organizer web client application",
                    //ClientUri = "http://identityserver.io",
                    //LogoUri = "https://pbs.twimg.com/profile_images/1612989113/Ki-hanja_400x400.png",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,
                    AccessTokenType = AccessTokenType.Jwt,

                    RedirectUris =
                    {
                        "http://localhost:4200/loading"
                    },

                    PostLogoutRedirectUris = { "http://localhost:4200" },
                    AllowedCorsOrigins = { "http://localhost:4200" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api.fullAccess"
                    }
                }
            };
        }
    }
}
