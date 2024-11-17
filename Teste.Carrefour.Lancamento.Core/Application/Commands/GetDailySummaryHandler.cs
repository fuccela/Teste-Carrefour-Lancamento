using MediatR;
using Teste.Carrefour.Lancamento.Application.Interfaces;

namespace Teste.Carrefour.Lancamento.Core.Application.Commands
{
    public class GetDailySummaryHandler : IRequestHandler<GetDailySummaryCommand, decimal>
    {
        private readonly IDailySummaryRepository _dailySummaryRepository;

        public GetDailySummaryHandler(IDailySummaryRepository dailySummaryRepository) => _dailySummaryRepository = dailySummaryRepository;

        public async Task<decimal> Handle(GetDailySummaryCommand request, CancellationToken cancellationToken)
        {
            var summary = await _dailySummaryRepository.GetByDateAsync(request.Date);
            return summary?.TotalBalance ?? 0m;
        }
    }
}
