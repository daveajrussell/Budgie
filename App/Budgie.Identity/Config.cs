using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
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
                new ApiResource(IdentityConstants.ApiName, IdentityConstants.ApiDescription)
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = IdentityConstants.ClientId,
                    ClientName = IdentityConstants.ClientName,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { "whatever the spa app uri is/callback.html" },
                    PostLogoutRedirectUris = { "whatever the spa app uri is/index.html" },
                    AllowedCorsOrigins = { "whatever the spa app uri is" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityConstants.ApiName
                    },
                }
            };
        }
    }
}