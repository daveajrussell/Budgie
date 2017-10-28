using Microsoft.EntityFrameworkCore;
using System;

namespace Core.Data.Abstractions
{
    public interface IRepositoryProvider
    {
        IRepository<T> GetRepositoryForEntityType<T>(DbContext context) where T : class;

        T GetRepository<T>(DbContext context, Func<DbContext, object> factory = null) where T : class;
    }
}