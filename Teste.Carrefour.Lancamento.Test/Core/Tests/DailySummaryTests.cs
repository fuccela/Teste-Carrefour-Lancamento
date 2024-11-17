using Teste.Carrefour.Lancamento.Core.Domain.Entities;
using Teste.Carrefour.Lancamento.Core.Domain.Enums;

namespace Teste.Carrefour.Lancamento.Test.Core.Tests
{
    [TestFixture]
    public class DailySummaryTests
    {
        [Test]
        public void DailySummary_Creation_ShouldSetDateAndInitializeProperties()
        {
            var date = DateTime.UtcNow.Date;

            var dailySummary = new DailySummary(date);

            Assert.That(dailySummary.Date, Is.EqualTo(date));
            Assert.That(dailySummary.TotalBalance, Is.EqualTo(0m));
            Assert.IsEmpty(dailySummary.Transactions);
        }

        [Test]
        public void AddTransaction_ShouldAddTransactionAndUpdateTotalBalance()
        {
            var dailySummary = new DailySummary(DateTime.UtcNow.Date);
            var transaction = new Transaction(100.00m, TransactionType.Credit);

            dailySummary.AddTransaction(transaction);

            Assert.That(dailySummary.Transactions.Count, Is.EqualTo(1));
            Assert.That(dailySummary.TotalBalance, Is.EqualTo(100.00m));
        }

        [Test]
        public void AddTransaction_ShouldThrowException_IfTransactionDateDoesNotMatch()
        {
            var dailySummary = new DailySummary(DateTime.UtcNow.Date);
            var transaction = new Transaction(100.00m, TransactionType.Credit);

            transaction.TransactionDate.AddDays(1);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                dailySummary.AddTransaction(transaction));

            Assert.That(ex.Message, Is.EqualTo("Transaction date must match the DailySummary date."));
        }
    }
}
