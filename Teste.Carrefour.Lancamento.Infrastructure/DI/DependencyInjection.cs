using Microsoft.Extensions.DependencyInjection;
using Teste.Carrefour.Lancamento.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Teste.Carrefour.Lancamento.Infrastructure.Persistence;
using Teste.Carrefour.Lancamento.Infrastructure.MessageQueue;
using RabbitMQ.Client;
using Teste.Carrefour.Lancamento.Core.Application.Services;


namespace Teste.Carrefour.Lancamento.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IDailySummaryRepository, DailySummaryRepository>();
            services.AddScoped<ITransactionProcessingService, TransactionProcessingService>();
            services.AddScoped<IRabbitMqProducer, RabbitMqProducer>();

            services.AddSingleton(sp =>
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri(configuration.GetConnectionString("RabbitMQConnection"))
                };

                var connectionTask = factory.CreateConnectionAsync();
                connectionTask.Wait();
                return connectionTask.Result;
            });
            
            services.AddHostedService<RabbitMqConsumer>();

            return services;
        }
    }
}
