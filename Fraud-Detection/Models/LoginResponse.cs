namespace FraudDetection.Models
{
    public class LoginResponse
    {
        public string Token { get; set; } = "";
        public string Message { get; set; } = "";
        public bool Success { get; set; }

        public User? User { get; set; }
    }
}