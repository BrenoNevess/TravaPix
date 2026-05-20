using System;

namespace FraudDetection.Models
{
    public class TransactionRecord
    {
        public string SenderCpf { get; set; } = "";

        public string ReceiverCpf { get; set; } = "";

        public decimal Amount { get; set; }

        public string Location { get; set; } = "";

        public string Description { get; set; } = "";

        public DateTime Date { get; set; }
    }
}