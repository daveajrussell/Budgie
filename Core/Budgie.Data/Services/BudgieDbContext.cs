using Budgie.Core;
using Budgie.Data.Abstractions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Budgie.Data.Services
{
    public class BudgieDbContext : IdentityDbContext<User, Role, int>, ICoreDbContext
    {
        /* Core */
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public BudgieDbContext(DbContextOptions<BudgieDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Transaction>().HasOne(sc => sc.SubCategory).WithMany(t => t.Transactions)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
