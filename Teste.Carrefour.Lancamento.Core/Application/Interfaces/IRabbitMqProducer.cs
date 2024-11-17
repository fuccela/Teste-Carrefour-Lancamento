using Teste.Carrefour.Lancamento.Core.Domain.Entities;

namespace Teste.Carrefour.Lancamento.Application.Interfaces
{
    public interface IRabbitMqProducer
    {
        Task<bool> SendMessageAsync(Transaction transaction);
    }
}
