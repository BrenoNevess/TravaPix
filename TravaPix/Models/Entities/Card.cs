namespace FraudDetection.Web.Models.Entities
{
    /// <summary>
    /// Cartão vinculado a um usuário (relação 1:1).
    /// </summary>
    public class Card
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string CardNumber { get; set; } = "";

        public string ExpiryDate { get; set; } = "";

        public decimal CreditLimit { get; set; }

        public decimal UsedLimit { get; set; }

        public User User { get; set; } = null!;
    }
}
