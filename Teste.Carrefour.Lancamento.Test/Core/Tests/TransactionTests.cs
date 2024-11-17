using Teste.Carrefour.Lancamento.Core.Domain.Entities;
using Teste.Carrefour.Lancamento.Core.Domain.Enums;

namespace Teste.Carrefour.Lancamento.Test.Core.Tests
{
    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public void Transaction_Creation_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var amount = 100.00m;
            var type = TransactionType.Credit;
            var timestamp = DateTime.UtcNow;

            // Act
            var transaction = new Transaction(amount, type);

            // Assert
            Assert.That(transaction.Amount, Is.EqualTo(amount));
            Assert.That(transaction.TransactionType, Is.EqualTo(type));
            Assert.That(transaction.Date, Is.EqualTo(timestamp.Date));
        }

        [Test]
        public void Transaction_ShouldThrowException_WhenTimestampIsNotUTC()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
                new Transaction(100.00m, TransactionType.Credit));

            Assert.That(ex.Message, Is.EqualTo("Timestamp must be in UTC."));
        }

        [Test]
        public void GetEffectiveAmount_ShouldReturnPositiveForCredit()
        {
            // Arrange
            var transaction = new Transaction(100.00m, TransactionType.Credit);

            // Act
            var result = transaction.GetEffectiveAmount();

            // Assert
            Assert.That(result, Is.EqualTo(100.00m));
        }

        [Test]
        public void GetEffectiveAmount_ShouldReturnNegativeForDebit()
        {
            // Arrange
            var transaction = new Transaction(50.00m, TransactionType.Debit);

            // Act
            var result = transaction.GetEffectiveAmount();

            // Assert
            Assert.That(result, Is.EqualTo(-50.00m));
        }
    }
}