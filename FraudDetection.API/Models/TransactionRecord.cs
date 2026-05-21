namespace FraudDetection.API.Models
{
    public class TransactionRecord
    {
        public string SenderCpf { get; set; } = string.Empty;

        public string ReceiverCpf { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Location { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public bool IsFraud { get; set; }

        public string RiskLevel { get; set; } =
            "SAFE";

        public string FraudReason { get; set; } =
            string.Empty;
    }
}