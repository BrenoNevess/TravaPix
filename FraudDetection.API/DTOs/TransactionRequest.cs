namespace FraudDetection.API.DTOs
{
    public class TransactionRequest
    {
        public string SenderCpf { get; set; } = "";

        public string ReceiverCpf { get; set; } = "";

        public decimal Amount { get; set; }

        public string Location { get; set; } = "";

        public string Description { get; set; } = "";
    }
}