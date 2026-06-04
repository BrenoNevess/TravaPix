using System.Text.RegularExpressions;

namespace FraudDetection.API.Services
{
    public static class EmailValidator
    {
        public static bool Validate(
            string email
        )
        {
            if(
                string.IsNullOrWhiteSpace(
                    email
                )
            )
            {
                return false;
            }

            return Regex.IsMatch(
                email,

                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"
            );
        }
    }
}