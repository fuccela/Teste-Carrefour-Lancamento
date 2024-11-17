using Teste.Carrefour.Lancamento.Core.Domain.Enums;

namespace Teste.Carrefour.Lancamento.Core.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public DateTime Date { get; private set; }
        public TransactionType TransactionType { get; private set; }
        public DailySummary DailySummary { get; private set; }

        public Transaction(decimal amount, TransactionType transactionType)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            TransactionType = transactionType;
            TransactionDate = DateTime.UtcNow;
            Date = DateTime.Now;

            if (TransactionDate.Kind != DateTimeKind.Utc)
            {
                throw new InvalidOperationException("Timestamp must be in UTC.");
            }
        }

        public decimal GetEffectiveAmount() => TransactionType == Enums.TransactionType.Credit ? Amount : -Amount;
    }
}
