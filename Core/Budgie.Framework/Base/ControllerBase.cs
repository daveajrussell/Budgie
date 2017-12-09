using Budgie.Framework.Facade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Budgie.Framework.Base
{
    public class ControllerBase : Controller
    {
        public Ticket Ticket { get; protected set; }

        public ControllerBase(IHttpContextAccessor httpContextAccessor)
        {
            Ticket = httpContextAccessor.HttpContext.Items["Ticket"] as Ticket;
        }
    }
}
