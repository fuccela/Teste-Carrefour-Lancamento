using System.Transactions;

namespace Teste.Carrefour.Lancamento.Application.Interfaces
{
    internal interface ITransactionProcessingService
    {
        Task ProcessTransactionAsync(Transaction transaction);
    }
}
