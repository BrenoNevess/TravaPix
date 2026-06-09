using Microsoft.AspNetCore.Mvc;

using FraudDetection.API.Data;
using FraudDetection.API.DTOs;
using FraudDetection.API.Models;
using FraudDetection.API.Services;

namespace FraudDetection.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController
        : ControllerBase
    {
        private readonly AppDbContext
            _context;

        public AuthController(
            AppDbContext context
        )
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register(
            RegisterRequest request
        )
        {
            bool cpfExists =
                _context.Users.Any(
                    u =>
                        u.Cpf ==
                        request.Cpf
                );

            if (cpfExists)
            {
                throw new Exception(
                    "CPF já cadastrado."
                );
            }

            bool emailExists =
                _context.Users.Any(
                    u =>
                        u.Email ==
                        request.Email
                );

                if(
                !EmailValidator.Validate(
                    request.Email
                )
            )
            {
                throw new Exception(
                    "O email precisa ser válido!"
                );
            }

            if (emailExists)
            {
                throw new Exception(
                    "Email já cadastrado."
                );
            }

            bool validPassword =
                PasswordValidator.Validate(
                    request.Password,
                    request.Name,
                    request.Cpf,
                    request.Email,
                    out string error
                );

            if (!validPassword)
            {
                throw new Exception(
                    error
                );
            }

            // Validação sem salvar o cvv

            if (
                string.IsNullOrWhiteSpace(
                    request.CardCvv
                )
                ||
                request.CardCvv.Length < 3
                ||
                request.CardCvv.Length > 4
            )
            {
                throw new Exception(
                    "CVV inválido."
                );
            }

            Guid userId = Guid.NewGuid();

            Card card =
                new Card
                {
                    Id = Guid.NewGuid(),

                    UserId = userId,

                    CardNumber =
                        request.CardNumber,

                    ExpiryDate =
                        request.ExpiryDate,

                    CreditLimit =
                        request.CreditLimit,

                    UsedLimit = 0
                };

        User user =
            new User
            {
                Id = userId,

                Name =
                    request.Name,

                Cpf =
                    request.Cpf,

                Email =
                    request.Email,

                Password =
                    request.Password,

                Role = "USER",

                Card = card
            };

            _context.Users.Add(
                user
            );

            _context.SaveChanges();

            return Ok(new
            {
                success=true,
                name=user.Name,
                cpf=user.Cpf,
                email=user.Email,
                role=user.Role,
                CreditLimit =
                user.Card != null
                ? user.Card.CreditLimit
                : 0
            });
        }

        [HttpPost("login")]
        public IActionResult Login(
            LoginRequest request
        )
        {
            string cpf =
                request.Cpf.Trim();

            string password =
                request.Password.Trim();

            User? user =
                _context.Users
                    .FirstOrDefault(
                        u =>
                            u.Cpf.Trim() ==
                            cpf
                    );

            if (user == null)
            {
                throw new Exception(
                    "Usuário não encontrado."
                );
            }

            if(
                user.Password.Trim()
                != password
            )
            {
                throw new Exception(
                    "Senha inválida."
                );
            }

            return Ok(
                new
                {
                    message =
                        "Login realizado.",

                    user.Name,
                    user.Cpf,
                    user.Email,
                    user.Role
                }
            );
        }
    }
}