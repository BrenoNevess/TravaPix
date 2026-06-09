namespace FraudDetection.API.Models
{
    public class FraudRecord
    {
        public string SenderCpf { get; set; } =
            string.Empty;

        public string ReceiverCpf { get; set; } =
            string.Empty;

        public decimal Amount { get; set; }

        public string Location { get; set; } =
            string.Empty;

        public DateTime Date { get; set; }

        public string RiskLevel { get; set; } =
            string.Empty;

        public string Reason { get; set; } =
            string.Empty;
    }
}