using System;

namespace FraudDetection.Models
{
    public class FraudRecord
    {
        public string TransactionCode { get; set; } = string.Empty;

        public string UserCpf { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Reason { get; set; } = string.Empty;

        public DateTime Date { get; set; }
    }
}