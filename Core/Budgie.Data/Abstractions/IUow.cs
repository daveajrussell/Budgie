using System.Threading.Tasks;
using Budgie.Core;

namespace Budgie.Data.Abstractions
{
    public interface IUow
    {
        void Commit();
        Task CommitAsync();

        IRepository<User> Users { get; }
        IRepository<Budget> Budgets { get; }
        IRepository<Category> Categories { get; }
        IRepository<Transaction> Transactions { get; }
    }
}