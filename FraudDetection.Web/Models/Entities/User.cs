namespace FraudDetection.Web.Models.Entities
{
    /// <summary>
    /// Usuário do sistema. Cada usuário possui exatamente um cartão associado.
    /// </summary>
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public string Cpf { get; set; } = "";

        public string Email { get; set; } = "";

        public string? Location { get; set; }

        public decimal? CreditLimit { get; set; }

        /// <summary>Hash BCrypt da senha (nunca a senha em texto puro).</summary>
        public string Password { get; set; } = "";

        public string Role { get; set; } = "USER";

        public Card Card { get; set; } = null!;
    }
}
