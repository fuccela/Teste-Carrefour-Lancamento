using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Teste.Carrefour.Lancamento.Core.Domain.Entities;

namespace Teste.Carrefour.Lancamento.Infrastructure.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {            
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<DailySummary> DailySummaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).IsRequired();
                entity.Property(e => e.TransactionType).IsRequired();
                entity.Property(e => e.TransactionDate).IsRequired();
                entity.Property(e => e.Date).IsRequired();
            });

            modelBuilder.Entity<DailySummary>(entity =>
            {
                entity.HasKey(e => e.Date);
                entity.Property(e => e.TotalBalance).IsRequired();

                entity.HasMany(e => e.Transactions)
                  .WithOne(e => e.DailySummary)
                  .HasPrincipalKey(e => e.Date)
                  .HasForeignKey(e => e.Date);
            });
        }
    }
}
