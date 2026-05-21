using Microsoft.AspNetCore.Mvc;

using FraudDetection.API.DTOs;
using FraudDetection.API.Models;
using FraudDetection.API.Repositories;
using FraudDetection.API.Services;

namespace FraudDetection.API.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register(
            RegisterRequest request
        )
        {
            bool cpfExists =
                UserRepository.Users.Any(
                    u => u.Cpf == request.Cpf
                );

            if (cpfExists)
            {
                return BadRequest(
                    new
                    {
                        message =
                            "CPF já cadastrado."
                    }
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
                return BadRequest(
                    new
                    {
                        message = error
                    }
                );
            }

            User user = new User
            {
                Id = Guid.NewGuid(),

                Name = request.Name,

                Cpf = request.Cpf,

                Email = request.Email,

                Password = request.Password,

                Role = "USER"
            };

            UserRepository.Users.Add(
                user
            );

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
                UserRepository.Users
                    .FirstOrDefault(
                        u =>
                            u.Cpf ==
                                request.Cpf &&
                            u.Password ==
                                request.Password
                    );

            if (user == null)
            {
                return Unauthorized(
                    new
                    {
                        message =
                            "CPF ou senha inválidos."
                    }
                );
            }

            return Ok(
                new
                {
                    message =
                        "Login realizado.",

                    user.Name,

                    user.Cpf,

                    user.Role
                }
            );
        }
    }
}