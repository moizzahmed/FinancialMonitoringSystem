namespace FinancialMonitoringSystem.Interfaces
{
    public interface IQueueService
    {
        void SendToQueue(string message);
    }
}
