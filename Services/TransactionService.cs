using FinancialMonitoringSystem.Interfaces;
using FinancialMonitoringSystem.Models;

namespace FinancialMonitoringSystem.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITenantSettingsService _tenantSettingsService;
        private readonly IQueueService _processingQueueService;
        private readonly IQueueService _holdingQueueService;

        public TransactionService(ITenantSettingsService tenantSettingsService,
                                  IQueueService processingQueueService,
                                  IQueueService holdingQueueService)
        {
            _tenantSettingsService = tenantSettingsService;
            _processingQueueService = processingQueueService;
            _holdingQueueService = holdingQueueService;
        }

        public bool ProcessTransaction(TransactionEvent transactionEvent)
        {
            var tenantSettings = _tenantSettingsService.GetTenantSettings(transactionEvent.TenantId);

            var isTransactionValid = EvaluateTransaction(transactionEvent, tenantSettings);

            if (isTransactionValid)
            {
                // Convert the transaction event to a string or relevant message format for the queue
                string message = ConvertTransactionToMessage(transactionEvent);
                _processingQueueService.SendToQueue(message);
                return true;
            }
            else
            {
                // Convert the transaction event to a string or relevant message format for the queue
                string message = ConvertTransactionToMessage(transactionEvent);
                _holdingQueueService.SendToQueue(message);
                return false;
            }
        }

        private string ConvertTransactionToMessage(TransactionEvent transactionEvent)
        {
            // Implement the logic to convert the TransactionEvent object to a string or JSON
            // This could simply be a JSON serialization
            return System.Text.Json.JsonSerializer.Serialize(transactionEvent);
        }
        private bool EvaluateTransaction(TransactionEvent transaction, TenantSettings settings)
        {
            // Check payment threshold
            if (transaction.Amount > settings.Thresholds.PerTransaction)
            {
                return false; // Transaction exceeds per-transaction limit
            }

            // Check country sanctions
            if (settings.CountrySanctions.SourceCountryCodes.Contains(transaction.SourceAccount.CountryCode) ||
                settings.CountrySanctions.DestinationCountryCodes.Contains(transaction.DestinationAccount.CountryCode))
            {
                return false; // Either source or destination country is sanctioned
            }

            // For simplicity, this example does not implement the daily velocity limit check.
            // Implementing this would typically require tracking the sum of transactions per day per account,
            // which could be done using a database or an in-memory data structure with appropriate lifecycle management.

            return true; // Transaction passes all checks
        }
    }
}
