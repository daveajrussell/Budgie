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
using Budgie.Core.Enums;

namespace Budgie.Api.Controllers
{
    [Route("api/[controller]")]
    public class BudgetsController : ApiControllerBase
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;

        public BudgetsController(IUow uow, IMapper mapper, ITokenResolverMiddleware tokenResolver)
        : base(tokenResolver)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{year:int}/{month:int}")]
        public async Task<IActionResult> GetBudget(int year, int month)
        {
            var budget = await _uow.Budgets.GetBudget(Token.UserId, year, month);

            if (budget == null)
            {
                budget = new Budget
                {
                    UserId = Token.UserId,
                    Year = year,
                    Month = month,
                    DateAdded = DateTime.UtcNow
                };

                var categories = _uow.Categories
                                     .GetAll()
                                     .Where(x => x.UserId == Token.UserId)
                                     .ToList();

                var incomes = categories
                                .Where(x => x.Type == CategoryType.Income)
                                .Select(x => new Income
                                {
                                    BudgetId = budget.Id,
                                    DateAdded = DateTime.UtcNow,
                                    CategoryId = x.Id,
                                    Total = x.Recurring && x.RecurringValue.HasValue ? x.RecurringValue.Value : 0,
                                    Date = x.RecurringDate
                                })
                                .ToList();

                await _uow.Incomes.AddRangeAsync(incomes);

                var outgoings = categories
                                    .Where(x => x.Type == CategoryType.Dedicated || x.Type == CategoryType.Variable)
                                    .Select(x => new Outgoing
                                    {
                                        BudgetId = budget.Id,
                                        DateAdded = DateTime.UtcNow,
                                        CategoryId = x.Id,
                                        Budgeted = x.Recurring && x.RecurringValue.HasValue ? x.RecurringValue.Value : 0,
                                        Date = x.RecurringDate
                                    })
                                    .ToList();

                await _uow.Outgoings.AddRangeAsync(outgoings);

                var savings = categories
                                .Where(x => x.Type == CategoryType.Savings)
                                .Select(x => new Saving
                                {
                                    BudgetId = budget.Id,
                                    DateAdded = DateTime.UtcNow,
                                    CategoryId = x.Id,
                                    Total = x.Recurring && x.RecurringValue.HasValue ? x.RecurringValue.Value : 0,
                                    Date = x.RecurringDate
                                })
                                .ToList();

                await _uow.Savings.AddRangeAsync(savings);

                budget.Incomes = incomes;
                budget.Outgoings = outgoings;
                budget.Savings = savings;

                await _uow.Budgets.AddAsync(budget);
                await _uow.CommitAsync();
            }

            var model = _mapper.Map<Budget, ApiBudget>(budget);

            return new JsonResult(model);
        }

        [HttpPut]
        [Route("{year:int}/{month:int}/add")]
        public async Task<IActionResult> AddTransaction(int year, int month, [FromBody] ApiTransaction model)
        {
            return await Task.FromResult(new JsonResult(0));
        }

        [HttpPatch]
        [Route("{year:int}/{month:int}/edit")]
        public async Task<IActionResult> EditTransaction(int year, int month, [FromBody] ApiTransaction model)
        {
            return await Task.FromResult(new JsonResult(0));
        }

        [HttpDelete]
        [Route("{year:int}/{month:int}/id:int")]
        public async Task<IActionResult> DeleteTransaction(int year, int month, int id)
        {
            return await Task.FromResult(new JsonResult(0));
        }
    }
}