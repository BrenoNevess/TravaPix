namespace FraudDetection.API.DTOs
{
    public class RegisterRequest
    {
        public string Name { get; set; } = "";

        public string Cpf { get; set; } = "";

        public string Email { get; set; } = "";

        public string Password { get; set; } = "";

        public string CardNumber { get; set; } = "";

        public string CardCvv { get; set; } = "";

        public string ExpiryDate { get; set; } = "";

        public decimal CreditLimit { get; set; }
    }
}