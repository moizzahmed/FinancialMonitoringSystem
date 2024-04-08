namespace FinancialMonitoringSystem.Models
{
    public class TenantSettings
    {
        public string TenantId { get; set; }
        internal VelocityLimits VelocityLimits { get; set; }
        internal PaymentThresholds Thresholds { get; set; }
        internal CountrySanctions CountrySanctions { get; set; }
    }
}
