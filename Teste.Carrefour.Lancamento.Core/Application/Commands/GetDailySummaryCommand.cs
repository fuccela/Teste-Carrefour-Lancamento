using MediatR;

namespace Teste.Carrefour.Lancamento.Core.Application.Commands
{
    public record GetDailySummaryCommand(DateTime Date) : IRequest<decimal>;
}
