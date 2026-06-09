namespace FraudDetection.Web.Models.Entities
{
    /// <summary>
    /// CPF de destinatário bloqueado temporariamente (8h) após uma transação de
    /// risco. Registra também o remetente que originou o bloqueio e o motivo.
    /// </summary>
    public class BlockedRecipient
    {
        public Guid Id { get; set; }

        public string ReceiverCpf { get; set; } = "";

        public string SenderCpf { get; set; } = "";

        public string Reason { get; set; } = "";

        public DateTime BlockedAt { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}
