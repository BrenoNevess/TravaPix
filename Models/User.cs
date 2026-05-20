namespace FraudDetection.Models
{
    public class User
    {
        public string Name { get; set; } = "";

        public string Cpf { get; set; } = "";

        public string Email { get; set; } = "";

        public string Password { get; set; } = "";

        public bool IsAdmin { get; set; }

        public Card Card { get; set; } = new();
    }
}