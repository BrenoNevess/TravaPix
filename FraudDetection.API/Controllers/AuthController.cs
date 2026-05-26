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

            User user =
                new User
                {
                    Id =
                        Guid.NewGuid(),

                    Name =
                        request.Name,

                    Cpf =
                        request.Cpf,

                    Email =
                        request.Email,

                    Password =
                        request.Password,

                    Role =
                        "USER"
                };

            _context.Users.Add(
                user
            );

            _context.SaveChanges();

            return Ok(
                new
                {
                    message =
                        "Usuário cadastrado com sucesso."
                }
            );
        }

        [HttpPost("login")]
        public IActionResult Login(
            LoginRequest request
        )
        {
            User? user =
                _context.Users
                    .FirstOrDefault(
                        u =>
                            u.Cpf ==
                            request.Cpf
                    );

            if (
                user == null
                ||
                user.Password !=
                    request.Password
            )
            {
                throw new Exception(
                    "CPF ou senha inválidos."
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