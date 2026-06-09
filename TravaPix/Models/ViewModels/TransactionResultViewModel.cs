using FraudDetection.Web.Models.Enums;

namespace FraudDetection.Web.Models.ViewModels
{
    /// <summary>
    /// Resultado da análise de uma transação, exibido após o processamento.
    /// </summary>
    public class TransactionResultViewModel
    {
        public FraudRiskLevel RiskLevel { get; set; }

        public IReadOnlyList<string> Alerts { get; set; } = new List<string>();

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
