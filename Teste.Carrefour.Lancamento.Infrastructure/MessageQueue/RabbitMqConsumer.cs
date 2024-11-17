using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Teste.Carrefour.Lancamento.Application.Interfaces;
using Teste.Carrefour.Lancamento.Core.Domain.Entities;

namespace Teste.Carrefour.Lancamento.Infrastructure.MessageQueue
{
    public class RabbitMqConsumer : BackgroundService
    {
        private readonly IChannel _channel;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RabbitMqConsumer> _logger;

        public RabbitMqConsumer(IConnection connection,
                                IServiceProvider serviceProvider, 
                                ILogger<RabbitMqConsumer> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;

            _channel = connection.CreateChannelAsync().Result;
            _channel.QueueDeclareAsync(queue: "transactions", durable: true, exclusive: false, autoDelete: false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var transaction = JsonConvert.DeserializeObject<Transaction>(message);

                    if (transaction != null)
                    {
                        _logger.LogInformation($"Processing transaction with ID: {transaction.Id}");

                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var transactionProcessingService = scope.ServiceProvider.GetRequiredService<ITransactionProcessingService>();
                            await transactionProcessingService.ProcessTransactionAsync(transaction);
                        }

                        await _channel.BasicAckAsync(ea.DeliveryTag, false);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message");
                    await _channel.BasicNackAsync(ea.DeliveryTag, false, requeue: false);
                }
            };

            _channel.BasicConsumeAsync(queue: "transactions", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.CloseAsync();
            base.Dispose();
        }
    }
}
