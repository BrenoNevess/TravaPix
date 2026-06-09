using System.Text.RegularExpressions;

namespace FraudDetection.Web.Services
{
    /// Validação simples de e-mail
    public static class EmailValidator
    {
        private static readonly Regex Pattern =
            new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public static bool Validate(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return Pattern.IsMatch(email);
        }
    }
}
