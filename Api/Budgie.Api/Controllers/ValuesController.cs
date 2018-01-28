using System.Collections.Generic;
using Budgie.Framework.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budgie.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ValuesController : ApiControllerBase
    {
        public ValuesController()
        {
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}