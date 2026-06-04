using FraudDetection.Web.Models.Enums;
using FraudDetection.Web.Services.Interfaces;

namespace FraudDetection.Web.Services
{
    /// Regras de detecção de fraude baseadas em valor e horário da transação.
    /// O nível final é sempre o de maior severidade entre as regras acionadas
    /// (um horário atípico nunca rebaixa uma transação de alto risco).
    public class FraudDetectionService : IFraudDetectionService
    {
        private const decimal HighRiskThreshold = 3000m;
        private const decimal SuspiciousThreshold = 1000m;

        public FraudAnalysisResult Analyze(decimal amount, DateTime when)
        {
            List<string> alerts = new();
            FraudRiskLevel risk = FraudRiskLevel.Safe;

            if (amount >= HighRiskThreshold)
            {
                risk = Escalate(risk, FraudRiskLevel.HighRisk);
                alerts.Add("Valor extremamente elevado.");
            }
            else if (amount >= SuspiciousThreshold)
            {
                risk = Escalate(risk, FraudRiskLevel.Suspicious);
                alerts.Add("Valor acima do normal.");
            }

            int hour = when.Hour;

            if (hour <= 5 || hour >= 23)
            {
                risk = Escalate(risk, FraudRiskLevel.Suspicious);
                alerts.Add("Horário atípico para transações.");
            }

            return new FraudAnalysisResult(risk, alerts);
        }

        private static FraudRiskLevel Escalate(FraudRiskLevel current, FraudRiskLevel candidate)
            => (FraudRiskLevel)Math.Max((int)current, (int)candidate);
    }
}