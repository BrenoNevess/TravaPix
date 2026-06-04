using Microsoft.EntityFrameworkCore;
using FraudDetection.API.Models;

namespace FraudDetection.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Card> Cards => Set<Card>();
        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // USERS

            modelBuilder
                .Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(150);

            modelBuilder
                .Entity<User>()
                .Property(u => u.Cpf)
                .HasMaxLength(14);

            modelBuilder
                .Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(255);
                
            modelBuilder
                .Entity<User>()
                .Property(u => u.Location)
                .HasMaxLength(255);

            modelBuilder
                .Entity<User>()
                .Property(u => u.Password)
                .HasMaxLength(255);

            modelBuilder
                .Entity<User>()
                .Property(u => u.Role)
                .HasMaxLength(30);

            modelBuilder
                .Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder
                .Entity<User>()
                .HasIndex(u => u.Cpf)
                .IsUnique();

            // CARDS

            modelBuilder
                .Entity<Card>()
                .Property(c => c.CardNumber)
                .HasMaxLength(255);

            modelBuilder
                .Entity<Card>()
                .Property(c => c.ExpiryDate)
                .HasMaxLength(5);

            modelBuilder
                .Entity<Card>()
                .Property(c => c.CreditLimit)
                .HasPrecision(18, 2);

            modelBuilder
                .Entity<Card>()
                .Property(c => c.UsedLimit)
                .HasPrecision(18, 2);

            modelBuilder
                .Entity<User>()
                .HasOne(u => u.Card)
                .WithOne(c => c.User)
                .HasForeignKey<Card>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // TRANSACTIONS

            modelBuilder
                .Entity<Transaction>()
                .Property(t => t.SenderCpf)
                .HasMaxLength(14);

            modelBuilder
                .Entity<Transaction>()
                .Property(t => t.ReceiverCpf)
                .HasMaxLength(14);

            modelBuilder
                .Entity<Transaction>()
                .Property(t => t.Location)
                .HasMaxLength(120);

            modelBuilder
                .Entity<Transaction>()
                .Property(t => t.Description)
                .HasMaxLength(500);

            modelBuilder
                .Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2);
        }
    }
}