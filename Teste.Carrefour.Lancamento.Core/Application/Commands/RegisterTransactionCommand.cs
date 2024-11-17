using MediatR;
using Teste.Carrefour.Lancamento.Core.Domain.Enums;

namespace Teste.Carrefour.Lancamento.Core.Application.Commands
{
    public record RegisterTransactionCommand(decimal Amount, TransactionType Type) : IRequest<bool>;
}
