using Microsoft.EntityFrameworkCore;
using Teste.Carrefour.Lancamento.Application.Interfaces;
using Teste.Carrefour.Lancamento.Core.Domain.Entities;

namespace Teste.Carrefour.Lancamento.Infrastructure.Persistence
{
    public class DailySummaryRepository : IDailySummaryRepository
    {
        private readonly DatabaseContext _context;

        public DailySummaryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<DailySummary> GetByDateAsync(DateTime date)
        {
            return await _context.DailySummaries
                .FirstOrDefaultAsync(ds => ds.Date == date);
        }

        public async Task SaveAsync(DailySummary dailySummary)
        {
            var existingSummary = await GetByDateAsync(dailySummary.Date);

            if (existingSummary == null)
            {
                _context.DailySummaries.Add(dailySummary);
            }
            else
            {
                _context.DailySummaries.Update(dailySummary);
            }

            await _context.SaveChangesAsync();
        }
    }
}
