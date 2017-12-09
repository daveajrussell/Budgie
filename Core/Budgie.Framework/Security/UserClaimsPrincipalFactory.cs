using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Budgie.Core;
using Budgie.Framework.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Budgie.Framework.Security
{
    public class UserClaimsPrincipalFactory : IUserClaimsPrincipalFactory<User>
    {
        private readonly UserManager<User> _userManager;

        public UserClaimsPrincipalFactory(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(BudgieClaimTypes.UniqueId, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, user.GetFullName())
            };

            var roles = await _userManager.GetRolesAsync(user);

            roles.ToList().ForEach(role =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            var claimsIdentities = new List<ClaimsIdentity>
            {
                new ClaimsIdentity(claims, "Cookies")
            };

            return new ClaimsPrincipal(claimsIdentities);
        }
    }
}
