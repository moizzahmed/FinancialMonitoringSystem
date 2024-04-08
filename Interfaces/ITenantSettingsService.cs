using FinancialMonitoringSystem.Models;

namespace FinancialMonitoringSystem.Interfaces
{
    public interface ITenantSettingsService
    {
        TenantSettings GetTenantSettings(string tenantId);
    }
}
