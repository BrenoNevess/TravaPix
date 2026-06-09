using System.ComponentModel.DataAnnotations;
using FraudDetection.Web.Models.Enums;

namespace FraudDetection.Web.Models.ViewModels
{
    /// <summary>
    /// Etapa de revisão/confirmação de uma transação. Os dados da transação vêm
    /// em campos ocultos da etapa anterior; o resultado da análise é recalculado
    /// no servidor (não confiamos nos valores postados de risco/decisão).
    /// </summary>
    public class TransactionConfirmViewModel
    {
        public string SenderCpf { get; set; } = "";
        public string ReceiverCpf { get; set; } = "";
        public decimal Amount { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }

        // Resultado da análise (apenas para exibição na tela de revisão).
        public FraudRiskLevel RiskLevel { get; set; }
        public FraudDecision Decision { get; set; }
        public IReadOnlyList<string> Alerts { get; set; } = new List<string>();
        public string? BlockReason { get; set; }

        // Entrada do usuário (exigida quando a transação é suspeita).
        [Display(Name = "Assinatura (digite seu nome completo)")]
        public string? SignatureName { get; set; }

        [Display(Name = "Estou ciente do risco")]
        public bool AcknowledgedRisk { get; set; }

        public bool RequiresSignature => Decision == FraudDecision.RequireConfirmation;
    }
}
