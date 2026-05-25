using System;

namespace FraudDetection.Models
{
    public class FraudRecord
    {
        public Guid Id { get; set; }

        public string SenderCpf { get; set; } = "";

        public string ReceiverCpf { get; set; } = "";

        public decimal Amount { get; set; }

        public string Location { get; set; } = "";

        public string Reason { get; set; } = "";

        public DateTime Date { get; set; }

        /*Nivel de Risco */

        public int RiskScore { get; set; }

        public FraudRiskLevel RiskLevel { get; set; }
    }
}