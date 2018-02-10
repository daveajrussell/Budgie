using System;
using System.Threading.Tasks;
using Budgie.Core;
using Budgie.Data.Abstractions;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Budgie.Data.Services
{
    public class Uow : IUow, IDisposable
    {
        private BudgieDbContext DbContext { get; }

        protected IRepositoryProvider RepositoryProvider { get; set; }

        /* Core */
        public IRepository<User> Users => GetStandardRepo<User>();
        public IBudgetRepository Budgets => GetRepo<IBudgetRepository>();
        public IRepository<Category> Categories => GetStandardRepo<Category>();
        public IRepository<Transaction> Transactions => GetStandardRepo<Transaction>();
        public IRepository<Income> Incomes => GetStandardRepo<Income>();
        public IRepository<Outgoing> Outgoings => GetStandardRepo<Outgoing>();
        public IRepository<Saving> Savings => GetStandardRepo<Saving>();

        public Uow(IRepositoryProvider repositoryProvider, BudgieDbContext budgieDbContext)
        {
            RepositoryProvider = repositoryProvider;
            DbContext = budgieDbContext;
        }

        protected T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>(DbContext);
        }

        protected IRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>(DbContext);
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext?.Dispose();
            }
        }
    }
}
