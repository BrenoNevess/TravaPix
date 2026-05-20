namespace FraudDetection.Models
{
    public class Card
    {
        public int Id { get; set; }

        public string CardNumber { get; set; } = "";

        public string CardHolder { get; set; } = "";

        public string ExpirationDate { get; set; } = "";

        public string CVV { get; set; } = "";

        public string Brand { get; set; } = "";
    }
}