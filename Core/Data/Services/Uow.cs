using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;
using Core.Data.Abstractions;
using Microsoft.EntityFrameworkCore.Design;
using Domain;

namespace Core.Data.Services
{
    public class Uow : IUow, IDisposable
    {
        private CoreDbContext DbContext { get; }

        protected IRepositoryProvider RepositoryProvider { get; set; }

        /* Core */
        public IRepository<User> Users => GetStandardRepo<User>();
        //public IRepository<Client> Clients => GetStandardRepo<Client>();
        //public ITweetRepository Tweets => GetRepo<ITweetRepository>();

        public Uow(IRepositoryProvider repositoryProvider, IDesignTimeDbContextFactory<CoreDbContext> context)
        {
            RepositoryProvider = repositoryProvider;

            //DbContext = context.CreateDbContext();
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
