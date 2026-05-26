using Microsoft.AspNetCore.Mvc;

using FraudDetection.API.Data;
using FraudDetection.API.DTOs;
using FraudDetection.API.Models;

namespace FraudDetection.API.Controllers
{
    [ApiController]

    [Route("api/users")]
    public class UserController
        : ControllerBase
    {
        private readonly AppDbContext
            _context;

        public UserController(
            AppDbContext context
        )
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<UserResponse>
                users =
                    _context.Users
                        .Select(
                            u =>
                                new UserResponse
                                {
                                    Name =
                                        u.Name,

                                    Cpf =
                                        u.Cpf,

                                    Email =
                                        u.Email,

                                    Role =
                                        u.Role
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
                _context.Users
                    .FirstOrDefault(
                        u =>
                            u.Cpf ==
                            cpf
                    );

            if (user == null)
            {
                throw new Exception(
                    "Usuário não encontrado."
                );
            }

            return Ok(
                new UserResponse
                {
                    Name =
                        user.Name,

                    Cpf =
                        user.Cpf,

                    Email =
                        user.Email,

                    Role =
                        user.Role
                }
            );
        }

        [HttpPost]
        public IActionResult Create(
            RegisterRequest request
        )
        {
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
                _context.Users
                    .FirstOrDefault(
                        u =>
                            u.Cpf ==
                            cpf
                    );

            if (user == null)
            {
                throw new Exception(
                    "Usuário não encontrado."
                );
            }

            user.Name =
                request.Name;

            user.Email =
                request.Email;

            user.Password =
                request.Password;

            _context.SaveChanges();

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
                _context.Users
                    .FirstOrDefault(
                        u =>
                            u.Cpf ==
                            cpf
                    );

            if (user == null)
            {
                throw new Exception(
                    "Usuário não encontrado."
                );
            }

            _context.Users.Remove(
                user
            );

            _context.SaveChanges();

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