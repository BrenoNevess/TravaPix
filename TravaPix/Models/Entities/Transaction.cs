using FraudDetection.Web.Models.Enums;

namespace FraudDetection.Web.Models.Entities
{
    /// <summary>
    /// Transação financeira analisada pelo motor de detecção de fraudes.
    /// </summary>
    public class Transaction
    {
        public Guid Id { get; set; }

        public string SenderCpf { get; set; } = "";

        public string ReceiverCpf { get; set; } = "";

        public decimal Amount { get; set; }

        public string Location { get; set; } = "";

        public string Description { get; set; } = "";

        public FraudRiskLevel RiskLevel { get; set; }

        /// <summary>True quando a transação foi barrada pelo motor (não concluída).</summary>
        public bool IsBlocked { get; set; }

        /// <summary>Nome assinado pelo usuário ao confirmar uma transação suspeita (auditoria).</summary>
        public string? SignedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
