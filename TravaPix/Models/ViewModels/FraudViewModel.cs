using FraudDetection.Web.Models.Entities;

namespace FraudDetection.Web.Models.ViewModels
{
    /// <summary>
    /// Transações marcadas como suspeitas ou de alto risco (visão do administrador).
    /// </summary>
    public class FraudViewModel
    {
        public IReadOnlyList<Transaction> Frauds { get; set; }
            = new List<Transaction>();

        public IReadOnlyList<BlockedRecipient> BlockedRecipients { get; set; }
            = new List<BlockedRecipient>();

        public int SuspiciousCount { get; set; }

        public int HighRiskCount { get; set; }
    }
}
