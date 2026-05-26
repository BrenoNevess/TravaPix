using Microsoft.EntityFrameworkCore;

using FraudDetection.API.Models;

namespace FraudDetection.API.Data
{
    public class AppDbContext
        : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext>
            options
        )
            : base(options)
        {
        }

        public DbSet<User>
            Users
            => Set<User>();

        public DbSet<Transaction>
            Transactions
            => Set<Transaction>();
    }
}