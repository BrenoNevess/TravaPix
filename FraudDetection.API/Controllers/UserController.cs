using Microsoft.AspNetCore.Mvc;

using FraudDetection.API.Models;

namespace FraudDetection.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private static List<User> users =
            new List<User>();

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(users);
        }

        [HttpGet("{cpf}")]
        public IActionResult GetByCpf(
            string cpf
        )
        {
            User? user =
                users.FirstOrDefault(
                    u => u.Cpf == cpf
                );

            if (user == null)
            {
                return NotFound(
                    new
                    {
                        message =
                            "Usuário não encontrado."
                    }
                );
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser(
            [FromBody] User user
        )
        {
            bool alreadyExists =
                users.Any(
                    u => u.Cpf == user.Cpf
                );

            if (alreadyExists)
            {
                return BadRequest(
                    new
                    {
                        message =
                            "CPF já cadastrado."
                    }
                );
            }

            users.Add(user);

            return Ok(
                new
                {
                    message =
                        "Usuário criado com sucesso."
                }
            );
        }

        [HttpPut("{cpf}")]
        public IActionResult UpdateUser(
            string cpf,
            [FromBody] User updatedUser
        )
        {
            User? existingUser =
                users.FirstOrDefault(
                    u => u.Cpf == cpf
                );

            if (existingUser == null)
            {
                return NotFound(
                    new
                    {
                        message =
                            "Usuário não encontrado."
                    }
                );
            }

            existingUser.Name =
                updatedUser.Name;

            existingUser.Email =
                updatedUser.Email;

            existingUser.Password =
                updatedUser.Password;

            existingUser.Role =
                updatedUser.Role;

            return Ok(
                new
                {
                    message =
                        "Usuário atualizado."
                }
            );
        }

        [HttpDelete("{cpf}")]
        public IActionResult DeleteUser(
            string cpf
        )
        {
            User? user =
                users.FirstOrDefault(
                    u => u.Cpf == cpf
                );

            if (user == null)
            {
                return NotFound(
                    new
                    {
                        message =
                            "Usuário não encontrado."
                    }
                );
            }

            users.Remove(user);

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