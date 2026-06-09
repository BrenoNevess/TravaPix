using FraudDetection.API.Models;

namespace FraudDetection.API.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public string SenderCpf { get; set; } = "";

        public string ReceiverCpf { get; set; } = "";

        public decimal Amount { get; set; }

        public string Location { get; set; } = "";

        public string Description { get; set; } = "";

        public FraudRiskLevel RiskLevel { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}