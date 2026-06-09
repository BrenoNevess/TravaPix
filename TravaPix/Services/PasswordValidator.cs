using System.Text.RegularExpressions;

namespace FraudDetection.Web.Services
{
    // Regras de força de senha: minimo de 8 caracteres, maiuscula, número,caractere especial e n pode conter nome, CPF ou e-mail
    public static class PasswordValidator
    {
        public static bool Validate(
            string password,
            string name,
            string cpf,
            string email,
            out string error)
        {
            error = "";

            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                error = "A senha deve ter no mínimo 8 caracteres.";
                return false;
            }

            if (!Regex.IsMatch(password, "[A-Z]"))
            {
                error = "A senha deve possuir letra maiúscula.";
                return false;
            }

            if (!Regex.IsMatch(password, "[0-9]"))
            {
                error = "A senha deve possuir número.";
                return false;
            }

            if (!Regex.IsMatch(password, "[\\W_]"))
            {
                error = "A senha deve possuir caractere especial.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(name) &&
                password.ToLower().Contains(name.ToLower()))
            {
                error = "A senha não pode conter seu nome.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(cpf) && password.Contains(cpf))
            {
                error = "A senha não pode conter o CPF.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(email) &&
                password.ToLower().Contains(email.ToLower()))
            {
                error = "A senha não pode conter o e-mail.";
                return false;
            }

            return true;
        }
    }
}
