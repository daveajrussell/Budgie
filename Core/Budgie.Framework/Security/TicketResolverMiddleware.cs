using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Budgie.Framework.Security
{
    public class TicketResolverMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public TicketResolverMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
            }
        }
    }
}
