﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace ForeignDocsRec2020.Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("FedrApi", "Foreign Education Documents Recognition API")
        };

        public static IEnumerable<Client> Clients => new Client[] 
        {
            new Client
            {
                ClientId = "FedrApiClient",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "FedrApi" }
            }
        };

        //public static IEnumerable<ApiResource> Apis => new ApiResource[]
        //{
        //    new ApiResource("FdrApi", "Foreign Documentss Recognition API")
        //};
    }
}