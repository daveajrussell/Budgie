using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Budgie.Core;
using Budgie.Core.Constants;
using Budgie.Framework.Facade.Middlewares;
using System.Security.Claims;

namespace Budgie.Framework.Security
{
    public class TokenResolverMiddleware : ITokenResolverMiddleware
    {
        private readonly HttpContext _context;
        private User _user;

        public TokenResolverMiddleware(IHttpContextAccessor accessor)
        {
            _context = accessor.HttpContext;
        }

        public async Task<User> ResolveAsync()
        {
            if (_user == null && _context.User.Identity.IsAuthenticated)
            {
                string userId = _context.User.FindFirstValue(BudgieClaimTypes.SubjectId);

                _user = new User
                {
                    Id = int.Parse(userId)
                };
            }

            return await Task.FromResult(_user);
        }
    }
}
