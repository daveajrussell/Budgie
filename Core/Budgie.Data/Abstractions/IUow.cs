using System.Threading.Tasks;
using Budgie.Core;

namespace Budgie.Data.Abstractions
{
    public interface IUow
    {
        void Commit();
        Task CommitAsync();

        IRepository<User> Users { get; }
        IBudgetRepository Budgets { get; }
        IRepository<Category> Categories { get; }
        IRepository<Income> Incomes { get; }
        IRepository<Outgoing> Outgoings { get; }
        IRepository<Saving> Savings { get; }
        IRepository<Transaction> Transactions { get; }
    }
}