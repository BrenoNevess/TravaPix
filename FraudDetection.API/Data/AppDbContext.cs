using Microsoft.EntityFrameworkCore;

using FraudDetection.API.Models;

namespace FraudDetection.API.Data
{
    public class AppDbContext
        : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options
        )
            : base(options)
        {
        }

        public DbSet<User>
            Users => Set<User>();

        public DbSet<Transaction>
            Transactions => Set<Transaction>();

        public DbSet<Card>
            Cards => Set<Card>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder
        )
        {
            base.OnModelCreating(
                modelBuilder
            );

            /*
             * RELAÇÃO USER -> CARD
             * 1 usuário possui 1 cartão
             */

            modelBuilder
                .Entity<User>()
                .HasOne(
                    u => u.Card
                )
                .WithOne(
                    c => c.User
                )
                .HasForeignKey<Card>(
                    c => c.UserId
                );

            /*
             * PRECISION DECIMAL
             */

            modelBuilder
                .Entity<Transaction>()
                .Property(
                    t => t.Amount
                )
                .HasPrecision(
                    18,
                    2
                );

            modelBuilder
                .Entity<Card>()
                .Property(
                    c => c.CreditLimit
                )
                .HasPrecision(
                    18,
                    2
                );

            modelBuilder
                .Entity<Card>()
                .Property(
                    c => c.UsedLimit
                )
                .HasPrecision(
                    18,
                    2
                );
        }
    }
}