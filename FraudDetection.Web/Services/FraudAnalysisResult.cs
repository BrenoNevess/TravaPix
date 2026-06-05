using FraudDetection.Web.Models.Enums;

namespace FraudDetection.Web.Services
{
    /// <summary>
    /// Resultado da análise: o nível de risco, a ação a tomar (decisão), os
    /// alertas que justificam e, quando bloqueada, o motivo do bloqueio.
    /// </summary>
    public record FraudAnalysisResult(
        FraudRiskLevel RiskLevel,
        FraudDecision Decision,
        IReadOnlyList<string> Alerts,
        string? BlockReason);
}
