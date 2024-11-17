using MediatR;
using Teste.Carrefour.Lancamento.Application.Interfaces;
using Teste.Carrefour.Lancamento.Core.Domain.Entities;

namespace Teste.Carrefour.Lancamento.Core.Application.Commands
{
    public class RegisterTransactionHandler : IRequestHandler<RegisterTransactionCommand, bool>
    {
        private readonly IRabbitMqProducer _rabbitMqProducer;

        public RegisterTransactionHandler(IRabbitMqProducer rabbitMqProducer) => _rabbitMqProducer = rabbitMqProducer;

        public async Task<bool> Handle(RegisterTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = new Transaction(request.Amount, request.Type);
            return await _rabbitMqProducer.SendMessageAsync(transaction);
        }
    }
}
