namespace FraudDetection.Web.Models.ViewModels
{
    /// <summary>
    /// Dados exibidos quando uma transação é bloqueada pelo motor.
    /// </summary>
    public class BlockedTransactionViewModel
    {
        public string ReceiverCpf { get; set; } = "";

        public decimal Amount { get; set; }

        public string BlockReason { get; set; } = "";

        public DateTime BlockedUntil { get; set; }

        public IReadOnlyList<string> Alerts { get; set; } = new List<string>();
    }
}
