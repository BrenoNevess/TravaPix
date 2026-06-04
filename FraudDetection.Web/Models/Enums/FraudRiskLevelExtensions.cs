namespace FraudDetection.Web.Models.Enums
{
    /// <summary>
    /// Conversões de apresentação do nível de risco (rótulo em português e
    /// classe CSS do badge). Centraliza o mapeamento usado pelas Views.
    /// </summary>
    public static class FraudRiskLevelExtensions
    {
        public static string ToPortuguese(this FraudRiskLevel level) => level switch
        {
            FraudRiskLevel.Safe => "Segura",
            FraudRiskLevel.Suspicious => "Suspeita",
            FraudRiskLevel.HighRisk => "Alto Risco",
            _ => "Desconhecido"
        };

        /// <summary>Classe CSS do badge: badge-safe / badge-warning / badge-danger.</summary>
        public static string ToBadgeClass(this FraudRiskLevel level) => level switch
        {
            FraudRiskLevel.Safe => "badge-safe",
            FraudRiskLevel.Suspicious => "badge-warning",
            FraudRiskLevel.HighRisk => "badge-danger",
            _ => "badge-muted"
        };
    }
}
