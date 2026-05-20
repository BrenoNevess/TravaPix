namespace FraudDetection.Models
{
    public class Card
    {
        public string CardNumber { get; set; } = "";

        public string Cvv { get; set; } = "";

        public string ExpiryDate { get; set; } = "";

        public decimal Limit { get; set; }
    }
}