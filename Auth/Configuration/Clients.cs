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
                

                ///////////////////////////////////////////
                // MVC Implicit Flow Samples
                //////////////////////////////////////////
                //new Client
                //{
                //    ClientId = "mvc.implicit",
                //    ClientName = "MVC Implicit",
                //    ClientUri = "http://identityserver.io",

                //    AllowedGrantTypes = GrantTypes.Implicit,
                //    AllowAccessTokensViaBrowser = true,

                //    RedirectUris =  { "http://localhost:44077/signin-oidc" },
                //    FrontChannelLogoutUri = "http://localhost:44077/signout-oidc",
                //    PostLogoutRedirectUris = { "http://localhost:44077/signout-callback-oidc" },

                //    AllowedScopes =
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        IdentityServerConstants.StandardScopes.Email,
                //        "api1", "api2.read_only"
                //    }
                //},

                ///////////////////////////////////////////
                // JS OIDC Sample
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "spa",
                    ClientName = "Angular2 client application",
                    ClientUri = "http://identityserver.io",
                    LogoUri = "https://pbs.twimg.com/profile_images/1612989113/Ki-hanja_400x400.png",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,
                    AccessTokenType = AccessTokenType.Jwt,

                    RedirectUris =
                    {
                        "http://localhost:4200/home"
                    },

                    PostLogoutRedirectUris = { "http://localhost:4200" },
                    AllowedCorsOrigins = { "http://localhost:5002", "http://localhost:4200" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api1", "api2.read_only", "api2.full_access"
                    }
                }
            };
        }
    }
}
