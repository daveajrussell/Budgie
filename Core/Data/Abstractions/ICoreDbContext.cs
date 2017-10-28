using Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Abstractions
{
    public interface ICoreDbContext
    {
        ///* Core */
        DbSet<User> Users { get; set; }
        //DbSet<Tweet> Tweets { get; }
    }
}
