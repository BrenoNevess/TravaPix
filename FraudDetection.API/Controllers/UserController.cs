using Microsoft.AspNetCore.Mvc;

using FraudDetection.API.DTOs;
using FraudDetection.API.Models;
using FraudDetection.API.Repositories;

namespace FraudDetection.API.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class UserController
        : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            List<UserResponse> users =
                UserRepository.Users
                .Select(
                    u =>
                        new UserResponse
                        {
                            Name = u.Name,
                            Cpf = u.Cpf,
                            Email = u.Email,
                            Role = u.Role
                        }
                )
                .ToList();

            return Ok(users);
        }

        [HttpGet("{cpf}")]
        public IActionResult GetByCpf(
            string cpf
        )
        {
            User? user =
                UserRepository.Users
                .FirstOrDefault(
                    u => u.Cpf == cpf
                );

            if (user == null)
            {
                throw new ArgumentException(
                    "Usuário não encontrado."
                );
            }

            return Ok(
                new UserResponse
                {
                    Name = user.Name,
                    Cpf = user.Cpf,
                    Email = user.Email,
                    Role = user.Role
                }
            );
        }

        [HttpPost]
        public IActionResult Create(
            RegisterRequest request
        )
        {
            bool exists =
                UserRepository.Users.Any(
                    u => u.Cpf == request.Cpf
                );

            if (exists)
            {
                throw new ArgumentException(
                    "CPF já existe."
                );
            }

            User user =
                new User
                {
                    Id = Guid.NewGuid(),

                    Name = request.Name,

                    Cpf = request.Cpf,

                    Email = request.Email,

                    Password =
                        request.Password,

                    Role = "USER"
                };

            UserRepository.Users.Add(
                user
            );

            return Ok(
                new
                {
                    message =
                        "Usuário criado."
                }
            );
        }

        [HttpPut("{cpf}")]
        public IActionResult Update(
            string cpf,
            RegisterRequest request
        )
        {
            User? user =
                UserRepository.Users
                .FirstOrDefault(
                    u => u.Cpf == cpf
                );

            if (user == null)
            {
                throw new ArgumentException(
                    "Usuário não encontrado."
                );
            }

            user.Name =
                request.Name;

            user.Email =
                request.Email;

            user.Password =
                request.Password;

            return Ok(
                new
                {
                    message =
                        "Usuário atualizado."
                }
            );
        }

        [HttpDelete("{cpf}")]
        public IActionResult Delete(
            string cpf
        )
        {
            User? user =
                UserRepository.Users
                .FirstOrDefault(
                    u => u.Cpf == cpf
                );

            if (user == null)
            {
                throw new ArgumentException(
                    "Usuário não encontrado."
                );
            }

            UserRepository.Users.Remove(
                user
            );

            return Ok(
                new
                {
                    message =
                        "Usuário removido."
                }
            );
        }
    }
}