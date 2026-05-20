using System;

namespace FraudDetection.Models
{
    public class TransactionRecord
    {
        public string TransactionCode { get; set; } = string.Empty;

        public string SenderCpf { get; set; } = string.Empty;

        public string ReceiverCpf { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string CardUsed { get; set; } = string.Empty;
    }
}