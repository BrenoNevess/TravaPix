using System.Collections.Generic;
using System.Linq;

using FraudDetection.Models;
using FraudDetection.Repositories;

namespace FraudDetection.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository;

        public UserService(
            IUserRepository repository
        )
        {
            userRepository = repository;
        }

        public List<User> GetAllUsers()
        {
            return userRepository.GetAll();
        }

        public User? GetByCpf(
            string cpf
        )
        {
            return userRepository
                .GetByCpf(cpf);
        }

        public void CreateUser(
            User user
        )
        {
            userRepository.Add(user);
        }

        public void UpdateUser(
            User updatedUser
        )
        {
            User? existingUser =
                userRepository.GetByCpf(
                    updatedUser.Cpf
                );

            if (existingUser == null)
            {
                return;
            }

            existingUser.Name =
                updatedUser.Name;

            existingUser.Email =
                updatedUser.Email;

            existingUser.Password =
                updatedUser.Password;

            existingUser.Role =
                updatedUser.Role;

            existingUser.Card =
                updatedUser.Card;
        }

        public void DeleteUser(
            string cpf
        )
        {
            User? user =
                userRepository.GetByCpf(cpf);

            if (user == null)
            {
                return;
            }

            userRepository.Remove(user);
        }
    }
}