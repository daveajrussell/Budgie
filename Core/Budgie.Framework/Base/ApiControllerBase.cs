using System;
using Budgie.Core;
using Budgie.Framework.Facade.Middlewares;
using Budgie.Framework.Models;
using Budgie.Framework.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Budgie.Framework.Base
{
    [SecurityHeaders]
    public abstract class ApiControllerBase
    {
        [ActionContext]
        public ActionContext ActionContext { get; set; }

        public HttpContext HttpContext => ActionContext?.HttpContext;

        public HttpRequest Request => ActionContext?.HttpContext?.Request;

        public HttpResponse Response => ActionContext?.HttpContext?.Response;

        public IServiceProvider Resolver => ActionContext?.HttpContext?.RequestServices;

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
    }
}