using Microsoft.EntityFrameworkCore;
using Teste.Carrefour.Lancamento.Application.Interfaces;
using Teste.Carrefour.Lancamento.Core.Domain.Entities;

namespace Teste.Carrefour.Lancamento.Infrastructure.Persistence
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DatabaseContext _context;

        public TransactionRepository(DatabaseContext context) => _context = context;

        public async Task AdicionarTransacaoAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionByDateAsync(DateTime date)
        {
            return await _context.Transactions
                .Where(t => t.TransactionDate == date).ToListAsync();
        }
    }
}
