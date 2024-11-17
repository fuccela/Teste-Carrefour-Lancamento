using System.Transactions;

namespace Teste.Carrefour.Lancamento.Application.Interfaces
{
    internal interface IRabbitMqProducer
    {
        Task<bool> SendMessageAsync(Transaction transaction);
    }
}
