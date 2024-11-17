using Teste.Carrefour.Lancamento.Core.Domain.Entities;

namespace Teste.Carrefour.Lancamento.Application.Interfaces
{
    public interface ITransactionProcessingService
    {
        Task ProcessTransactionAsync(Transaction transaction);
    }
}
