using System;
using System.Threading.Tasks;
using Data.Abstractions;
using Microsoft.EntityFrameworkCore.Design;
using Domain;

namespace Data.Services
{
    public class Uow : IUow, IDisposable
    {
        private CoreDbContext DbContext { get; }

        protected IRepositoryProvider RepositoryProvider { get; set; }

        /* Core */
        public IRepository<User> Users => GetStandardRepo<User>();
        public IRepository<Account> Accounts => GetStandardRepo<Account>();
        public IRepository<Budget> Budgets => GetStandardRepo<Budget>();
        public IRepository<Category> Categories => GetStandardRepo<Category>();
        public IRepository<Sheet> Sheets => GetStandardRepo<Sheet>();
        public IRepository<SubCategory> SubCategories => GetStandardRepo<SubCategory>();
        public IRepository<Transaction> Transactions => GetStandardRepo<Transaction>();
        public IRepository<Role> Roles => GetStandardRepo<Role>();

        public Uow(IRepositoryProvider repositoryProvider, IDesignTimeDbContextFactory<CoreDbContext> context)
        {
            RepositoryProvider = repositoryProvider;

            DbContext = context.CreateDbContext(new string[] { });
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
