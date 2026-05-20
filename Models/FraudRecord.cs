using System;

namespace FraudDetection.Models
{
    public class FraudRecord
    {
        public string SenderCpf { get; set; } = "";

        public string ReceiverCpf { get; set; } = "";

        public decimal Amount { get; set; }

        public string Location { get; set; } = "";

        public string Reason { get; set; } = "";

        public DateTime Date { get; set; }
    }
}