using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Abstractions
{
    public interface ICoreDbContext
    {
        /* Core */
        DbSet<User> Users { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Budget> Budgets { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Sheet> Sheets { get; set; }
        DbSet<SubCategory> SubCategories { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<Role> Roles { get; set; }
    }
}
