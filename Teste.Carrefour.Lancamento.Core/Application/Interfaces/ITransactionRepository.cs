namespace Teste.Carrefour.Lancamento.Application.Interfaces
{
    using Core.Domain.Entities;

    public interface ITransactionRepository
    {
        Task AdicionarTransacaoAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactionByDateAsync(DateTime date);
    }
}
