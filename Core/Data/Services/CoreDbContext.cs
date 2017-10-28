using Core.Data.Abstractions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Services
{
    public class CoreDbContext : DbContext, ICoreDbContext
    {
        /* Core */
        public DbSet<User> Users { get; set; }

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
