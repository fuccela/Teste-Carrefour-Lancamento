namespace Teste.Carrefour.Lancamento.Application.Interfaces
{
    internal interface IDailySummaryRepository
    {
        Task<DailySummary> GetByDateAsync(DateTime date);
        Task SaveAsync(DailySummary dailySummary);
    }
}
