namespace FraudDetection.API.DTOs
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public string Cpf { get; set; } = "";

        public string Email { get; set; } = "";

        public string Role { get; set; } = "";
    }
}