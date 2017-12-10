using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using Budgie.Framework.Security;

namespace Budgie.Identity
{
    public class Config
    {
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

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = BudgieIdentityConstants.ClientId,
                    ClientName = BudgieIdentityConstants.ClientName,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { "https://localhost:44372/callback.html" },
                    PostLogoutRedirectUris = { "https://localhost:44372/index.html" },
                    AllowedCorsOrigins = { "https://localhost:44372" },

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