using Budgie.Core;
using Budgie.Framework.Facade;
using Budgie.Framework.Facade.Middlewares;
using Budgie.Framework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Budgie.Framework.Base
{
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

                };
            }
        }

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
        }
    }
}
