using System.Threading.Tasks;
using Budgie.Core;
using Budgie.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class BudgetRepository : EFRepository<Budget>, IBudgetRepository
{
    public BudgetRepository(DbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Budget> GetBudget(int userId, int year, int month)
    {
        return await DbSet
                .Include(i => i.Incomes).ThenInclude(c => c.Category)
                .Include(o => o.Outgoings).ThenInclude(c => c.Category)
                .Include(s => s.Savings).ThenInclude(c => c.Category)
                .Where(u => u.UserId == userId)
                .Where(y => y.Year == year)
                .Where(m => m.Month == month)
                .FirstOrDefaultAsync();
    }
}