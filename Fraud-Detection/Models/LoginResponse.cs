namespace FraudDetection.Models
{
    public class LoginResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; } = "";

        public string Name { get; set; } = "";

        public string Cpf { get; set; } = "";

        public string Email { get; set; } = "";

        public string Role { get; set; } = "";
    }
}