using Budgie.Core;
using Budgie.Core.Contracts.Security;
using Budgie.Framework.Models;
using Budgie.Framework.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Budgie.Framework.Base
{
    [SecurityHeaders]
    public class BaseController : Controller
    {
        private User _currentUser;

        public Token Token
        {
            get
            {
                if (_currentUser == null)
                    _currentUser = HttpContext.RequestServices.GetRequiredService<ITokenResolverMiddleware>().ResolveAsync().Result;

                return new Token
                {
                    UserId = _currentUser.Id
                };
            }
        }

        public BaseController(IHttpContextAccessor httpContextAccessor) { }
    }
}