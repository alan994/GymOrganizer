﻿using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth
{
    public class Config
    {
        public static IEnumerable<Client> Clients = new List<Client>
        {
            new Client
            {
                ClientId = "spa",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,
                RedirectUris = {
                    "http://localhost:4200/home"                    
                },
                PostLogoutRedirectUris = { "http://localhost:4200" },
                AllowedScopes = { "openid", "profile", "email", "api1" },
                AllowedCorsOrigins = { "http://localhost:5002", "http://localhost:4200" }
            },
        };

        public static IEnumerable<IdentityResource> IdentityResources = new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

        public static IEnumerable<ApiResource> Apis = new List<ApiResource>
        {
            new ApiResource("api1", "My API 1")
        };
    }
}
