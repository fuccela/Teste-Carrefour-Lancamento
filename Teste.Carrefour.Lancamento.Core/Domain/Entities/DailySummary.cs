namespace Teste.Carrefour.Lancamento.Core.Domain.Entities
{
    public class DailySummary
    {
        public DateTime Date { get; private set; }
        public decimal TotalBalance { get; private set; }
        public ICollection<Transaction> Transactions { get; private set; } = [];

        public DailySummary(DateTime date)
        {
            Date = date;
            TotalBalance = 0m;
        }

        public void AddTransaction(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            Transactions.Add(transaction);
            TotalBalance += transaction.GetEffectiveAmount();
        }
    }
}