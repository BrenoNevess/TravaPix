using FraudDetection.Web.Models.Enums;

namespace FraudDetection.Web.Services
{
    /// Resultado da analise de risco, nivel calculado e os alertas que o justificam
    public record FraudAnalysisResult(
        FraudRiskLevel RiskLevel,
        IReadOnlyList<string> Alerts);
}
