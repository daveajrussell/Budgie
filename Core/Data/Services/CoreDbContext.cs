using Data.Abstractions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Services
{
    public class CoreDbContext : DbContext, ICoreDbContext
    {
        /* Core */
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Role> Roles { get; set; }

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
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
