using FinancialMonitoringSystem.Models;

namespace FinancialMonitoringSystem.Interfaces
{
    public interface ITransactionService
    {
        bool ProcessTransaction(TransactionEvent transactionEvent);
    }
}
