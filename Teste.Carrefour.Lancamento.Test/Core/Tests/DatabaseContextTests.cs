using Microsoft.EntityFrameworkCore;
using Teste.Carrefour.Lancamento.Core.Domain.Entities;
using Teste.Carrefour.Lancamento.Core.Domain.Enums;
using Teste.Carrefour.Lancamento.Infrastructure.Persistence;

namespace Teste.Carrefour.Lancamento.Test.Core.Tests
{
    [TestFixture]
    public class DatabaseContextTests
    {
        private DatabaseContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            return new DatabaseContext(options);
        }

        [Test]
        public void DatabaseContext_ShouldSaveAndRetrieveTransactions()
        {
            using var context = CreateDbContext();
            var transaction = new Transaction(100.00m, TransactionType.Credit);

            context.Transactions.Add(transaction);
            context.SaveChanges();

            var retrieved = context.Transactions.FirstOrDefault();
            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(transaction.Id));
        }

        [Test]
        public void DatabaseContext_ShouldSaveAndRetrieveDailySummaries()
        {
            using var context = CreateDbContext();
            var dailySummary = new DailySummary(DateTime.UtcNow.Date);
            context.DailySummaries.Add(dailySummary);
            context.SaveChanges();

            var retrieved = context.DailySummaries.Include(ds => ds.Transactions).FirstOrDefault();

            Assert.That(retrieved, Is.Not.Null);
            Assert.That(retrieved.Date, Is.EqualTo(dailySummary.Date));
        }
    }
}
