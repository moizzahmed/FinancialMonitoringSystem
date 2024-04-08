using FinancialMonitoringSystem.Interfaces;
using FinancialMonitoringSystem.Models;

namespace FinancialMonitoringSystem.Services
{
    public class TenantSettingsService : ITenantSettingsService
    {
        // This method should be implemented to retrieve the actual tenant settings.
        public TenantSettings GetTenantSettings(string tenantId)
        {
            // For simplicity, returning a new object. Replace with actual logic.
            return new TenantSettings
            {
                TenantId = tenantId,
                VelocityLimits = new VelocityLimits { Daily = 2500 },
                Thresholds = new PaymentThresholds { PerTransaction = 1500 },
                CountrySanctions = new CountrySanctions
                {
                    SourceCountryCodes = new List<string> { "AFG", "BLR", "BIH", "IRQ", "KEN", "RUS" },
                    DestinationCountryCodes = new List<string> { "AFG", "BLR", "BIH", "IRQ", "KEN", "RUS", "TKM", "UGA" }
                }
            };
        }
    }
}
