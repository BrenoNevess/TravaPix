using FraudDetection.Web.Data;
using FraudDetection.Web.Exceptions;
using FraudDetection.Web.Models.Entities;
using FraudDetection.Web.Models.ViewModels;
using FraudDetection.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FraudDetection.Web.Services
{
    // Cadastro e login
    // Senhas armazenadas com hash BCrypt
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterAsync(RegisterViewModel model)
        {
            string cpf = model.Cpf.Trim();
            string email = model.Email.Trim();

            if (await _context.Users.AnyAsync(u => u.Cpf == cpf))
            {
                throw new DomainException("CPF já cadastrado.");
            }

            if (!EmailValidator.Validate(email))
            {
                throw new DomainException("O e-mail precisa ser válido.");
            }

            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                throw new DomainException("E-mail já cadastrado.");
            }

            bool validPassword = PasswordValidator.Validate(
                model.Password,
                model.Name,
                cpf,
                email,
                out string passwordError);

            if (!validPassword)
            {
                throw new DomainException(passwordError);
            }

            if (model.Password != model.ConfirmPassword)
            {
                throw new DomainException("As senhas precisam ser iguais.");
            }

            ValidateCvv(model.CardCvv);

            Guid userId = Guid.NewGuid();

            Card card = new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CardNumber = model.CardNumber.Trim(),
                ExpiryDate = model.ExpiryDate.Trim(),
                CreditLimit = model.CreditLimit,
                UsedLimit = 0
            };

            // Cadastro

            User user = new()
            {
                Id = userId,
                Name = model.Name.Trim(),
                Cpf = cpf,
                Email = email,
                Location = string.IsNullOrWhiteSpace(model.Location)
                    ? null
                    : model.Location.Trim(),
                CreditLimit = model.CreditLimit,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "USER",
                Card = card
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> ValidateCredentialsAsync(string cpf, string password)
        {
            cpf = cpf.Trim();

            User? user = await _context.Users
                .FirstOrDefaultAsync(u => u.Cpf == cpf);

            if (user is null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            if (!BCrypt.Net.BCrypt.Verify(password.Trim(), user.Password))
            {
                throw new DomainException("Senha inválida.");
            }

            return user;
        }

        private static void ValidateCvv(string cvv)
        {
            cvv = cvv.Trim();

            if (string.IsNullOrWhiteSpace(cvv) || cvv.Length < 3 || cvv.Length > 4)
            {
                throw new DomainException("CVV inválido.");
            }
        }
    }
}
