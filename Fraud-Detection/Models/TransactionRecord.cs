using System;

namespace FraudDetection.Models
{
    public class TransactionRecord
    {
        public Guid Id { get; set; }

        public string SenderCpf { get; set; } = "";

        public string ReceiverCpf { get; set; } = "";

        public decimal Amount { get; set; }

        public string Location { get; set; } = "";

        public string Description { get; set; } = "";

        public DateTime Date { get; set; }

        /* Resultado da analise */

        public int RiskScore { get; set; }

        public FraudRiskLevel RiskLevel { get; set; }

        public bool IsFraud { get; set; }
    }
}