namespace FraudDetection.API.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public string Cpf { get; set; } = "";

        public string Email { get; set; } = "";

        public string? Location { get; set; }

        public string Password { get; set; } = "";

        public string Role { get; set; } = "USER";

        public Card Card { get; set; } = null!;
    }
}