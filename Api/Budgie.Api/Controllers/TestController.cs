using System.Collections.Generic;
using Budgie.Framework.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Budgie.Data.Abstractions;
using System.Linq;
using Budgie.Core;

namespace Budgie.Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    public class TestController : ApiControllerBase
    {
        private readonly IUow _uow;

        public TestController(IUow uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public IEnumerable<Budget> Get()
        {
            return _uow.Budgets.GetAll().ToList();
        }
    }
}