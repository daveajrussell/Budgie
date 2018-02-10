using System;
using System.Collections.Generic;
using Budgie.Framework.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Budgie.Data.Abstractions;
using System.Linq;
using Budgie.Core;
using AutoMapper;
using System.Threading.Tasks;
using Budgie.Framework.Facade.Middlewares;

namespace Budgie.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : ApiControllerBase
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;

        public CategoriesController(IUow uow, IMapper mapper, ITokenResolverMiddleware tokenResolver)
        : base(tokenResolver)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _uow.Categories.GetAll().Where(x => x.UserId == Token.UserId).ToList();
            var model = _mapper.Map<IList<Category>, IEnumerable<ApiCategory>>(categories);

            return new JsonResult(model);
        }

        [HttpPut]
        [Route("add")]
        public async Task<IActionResult> AddCategory([FromBody] ApiCategory model)
        {
            var category = new Category
            {
                UserId = Token.UserId,
                Name = model.Name,
                Colour = model.Colour,
                Type = model.Type,
                Recurring = model.Recurring,
                DateAdded = DateTime.UtcNow
            };

            if (category.Recurring)
            {
                category.RecurringDate = model.RecurringDate;
                category.RecurringValue = model.RecurringValue;
            }

            _uow.Categories.Add(category);
            await _uow.CommitAsync();

            model = _mapper.Map<Category, ApiCategory>(category);

            return new JsonResult(model);
        }

        [HttpPatch]
        [Route("edit")]
        public async Task<IActionResult> EditCategory([FromBody] ApiCategory model)
        {
            var category = await _uow.Categories.GetByIdAsync(model.Id);

            category.Name = model.Name;
            category.Colour = model.Colour;
            category.Type = model.Type;
            category.Recurring = model.Recurring;
            category.DateModified = DateTime.UtcNow;

            if (category.Recurring)
            {
                category.RecurringDate = model.RecurringDate;
                category.RecurringValue = model.RecurringValue;
            }
            else
            {
                category.RecurringDate = null;
                category.RecurringValue = null;
            }

            _uow.Categories.Update(category);
            await _uow.CommitAsync();

            model = _mapper.Map<Category, ApiCategory>(category);

            return new JsonResult(model);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task DeleteCategory(int id)
        {
            _uow.Categories.Delete(id);
            await _uow.CommitAsync();
        }
    }
}