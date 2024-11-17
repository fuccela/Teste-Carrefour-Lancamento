using Microsoft.Extensions.Logging;
using Teste.Carrefour.Lancamento.Application.Interfaces;
using Teste.Carrefour.Lancamento.Core.Domain.Entities;

namespace Teste.Carrefour.Lancamento.Core.Application.Services
{
    public class TransactionProcessingService : ITransactionProcessingService
    {
        private readonly IDailySummaryRepository _dailySummaryRepository;
        private readonly ILogger<TransactionProcessingService> _logger;

        public TransactionProcessingService(IDailySummaryRepository dailySummaryRepository, ILogger<TransactionProcessingService> logger)
        {
            _dailySummaryRepository = dailySummaryRepository;
            _logger = logger;
        }

        public async Task ProcessTransactionAsync(Transaction transaction)
        {
            var dailySummary = _dailySummaryRepository.GetByDateAsync(transaction.TransactionDate).Result;

            dailySummary ??= new DailySummary(transaction.TransactionDate);

            dailySummary.AddTransaction(transaction);
            await _dailySummaryRepository.SaveAsync(dailySummary);

            _logger.LogInformation($"Daily summary updated for date {transaction.TransactionDate}: Total Balance {dailySummary.TotalBalance}.");
        }
    }
}
