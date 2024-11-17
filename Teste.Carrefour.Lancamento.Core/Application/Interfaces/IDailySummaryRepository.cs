using Teste.Carrefour.Lancamento.Core.Domain.Entities;

namespace Teste.Carrefour.Lancamento.Application.Interfaces
{
    public interface IDailySummaryRepository
    {
        Task<DailySummary> GetByDateAsync(DateTime date);
        Task SaveAsync(DailySummary dailySummary);
    }
}
