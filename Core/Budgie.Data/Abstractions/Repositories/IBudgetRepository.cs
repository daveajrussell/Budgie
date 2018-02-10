using System.Threading.Tasks;
using Budgie.Core;
using Budgie.Data.Abstractions;

public interface IBudgetRepository : IRepository<Budget>
{
    Task<Budget> GetBudget(int userId, int year, int month);
}