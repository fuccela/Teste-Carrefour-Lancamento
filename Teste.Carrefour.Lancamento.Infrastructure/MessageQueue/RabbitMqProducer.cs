using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using Teste.Carrefour.Lancamento.Application.Interfaces;
using Teste.Carrefour.Lancamento.Core.Domain.Entities;

namespace Teste.Carrefour.Lancamento.Infrastructure.MessageQueue
{
    public class RabbitMqProducer : IRabbitMqProducer
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        public RabbitMqProducer(IConnection connection)
        {
            _connection = connection;
            _channel = _connection.CreateChannelAsync().Result;
            _channel.QueueDeclareAsync(queue: "transactions", durable: true, exclusive: false, autoDelete: false);
        }

        public Task<bool> SendMessageAsync(Transaction transaction)
        {
            var message = JsonConvert.SerializeObject(transaction);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublishAsync(exchange: "", routingKey: "transactions", body: body);

            return Task.FromResult(true);
        }
    }
}
