namespace FraudDetection.API.Models
{
    public class Card
    {
        public Guid Id { get; set; }

        public string LastFourDigits { get; set; } = "";

        public string EncryptedNumber { get; set; } = "";

        public decimal CreditLimit { get; set; }

        public decimal UsedLimit { get; set; }

        public string Brand { get; set; } = "";
    }
}