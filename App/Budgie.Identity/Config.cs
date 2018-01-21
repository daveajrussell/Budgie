using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using Budgie.Framework.Security;
using Microsoft.Extensions.Configuration;

namespace Budgie.Identity
{
    public class Config
    {
        private const string AppBaseUri = "AppSettings:AppBaseUri";

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(BudgieIdentityConstants.ApiName, BudgieIdentityConstants.ApiDescription)
            };
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = BudgieIdentityConstants.ClientId,
                    ClientName = BudgieIdentityConstants.ClientName,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration[AppBaseUri]}/callback" },
                    PostLogoutRedirectUris = { $"{configuration[AppBaseUri]}/home" },
                    AllowedCorsOrigins = { $"{configuration[AppBaseUri]}" },

                    RequireConsent = false,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        BudgieIdentityConstants.ApiName
                    }
                }
            };
        }
    }
}