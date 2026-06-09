using System.Text.RegularExpressions;

namespace FraudDetection.API.Services
{
    public static class PasswordValidator
    {
        public static bool Validate(
            string password,
            string name,
            string cpf,
            string email,
            out string error
        )
        {
            error = "";

            if (password.Length < 8)
            {
                error =
                    "A senha deve ter no mínimo 8 caracteres.";

                return false;
            }

            if (
                !Regex.IsMatch(
                    password,
                    @"[A-Z]"
                )
            )
            {
                error =
                    "A senha deve possuir letra maiúscula.";

                return false;
            }

            if (
                !Regex.IsMatch(
                    password,
                    @"[0-9]"
                )
            )
            {
                error =
                    "A senha deve possuir número.";

                return false;
            }

            if (
                !Regex.IsMatch(
                    password,
                    @"[\W_]"
                )
            )
            {
                error =
                    "A senha deve possuir caractere especial.";

                return false;
            }

            if (
                password.ToLower()
                    .Contains(name.ToLower())
            )
            {
                error =
                    "A senha não pode conter seu nome.";

                return false;
            }

            if (
                password.Contains(cpf)
            )
            {
                error =
                    "A senha não pode conter CPF.";

                return false;
            }

            if (
                password.ToLower()
                    .Contains(email.ToLower())
            )
            {
                error =
                    "A senha não pode conter email.";

                return false;
            }

            return true;
        }
    }
}