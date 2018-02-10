using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Budgie.Core;
using Budgie.Core.Constants;
using Budgie.Framework.Facade.Middlewares;
using System.Security.Claims;

namespace Budgie.Framework.Security
{
    public class DevTokenResolverMiddleware : ITokenResolverMiddleware
    {
        private User _user;

        public DevTokenResolverMiddleware()
        {
        }

        public async Task<User> ResolveAsync()
        {
            _user = new User
            {
                Id = 1
            };

            return await Task.FromResult(_user);
        }
    }
}
